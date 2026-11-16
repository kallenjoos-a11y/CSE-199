#!/usr/bin/env bash

set -u

api_pid=""
web_pid=""

cleanup() {
  trap - INT TERM EXIT
  stop_tree() {
    local parent_pid="$1"
    local child_pid

    for child_pid in $(pgrep -P "$parent_pid" 2>/dev/null || true); do
      stop_tree "$child_pid"
    done

    kill "$parent_pid" 2>/dev/null || true
  }

  if [ -n "$api_pid" ]; then stop_tree "$api_pid"; fi
  if [ -n "$web_pid" ]; then stop_tree "$web_pid"; fi
  wait 2>/dev/null || true
}

trap cleanup INT TERM EXIT

echo "Starting API at http://localhost:5069"
dotnet run --project backend/BeThere/BeThere.csproj --launch-profile http &
api_pid=$!

echo "Starting React app at http://localhost:5173"
npm --prefix frontend run dev -- --host localhost &
web_pid=$!

echo "Both services are starting. Press Ctrl+C to stop both."

while kill -0 "$api_pid" 2>/dev/null && kill -0 "$web_pid" 2>/dev/null; do
  sleep 1
done

echo "One of the services stopped; shutting down the other."
