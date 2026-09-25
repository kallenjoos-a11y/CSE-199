import threading
import time

def worker(name, delay):
    print(f"{name} starting")

    print(f"{name} finishing")

if __name__ == "__main__":
    configs = [("A", 1), ("B", 2), ("C", 1)]
    threads = []

    for name, value in configs:
        t = threading.Thread(target=worker, args=(name, value))
        threads.append(t)

    for t in threads:
        t.start()

    for t in threads:
        t.join()