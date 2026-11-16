function FullSchedule({ meet, onBack }) {
  const participantsById = new Map(
    meet.participants.map((participant) => [participant.id, participant])
  )

  const activities = [...meet.activities].sort(
    (firstActivity, secondActivity) =>
      new Date(firstActivity.startTime) - new Date(secondActivity.startTime)
  )

  function formatActivityTime(startTime) {
    return new Intl.DateTimeFormat('en-US', {
      dateStyle: 'medium',
      timeStyle: 'short'
    }).format(new Date(startTime))
  }

  return (
    <section id="center" className="full-schedule">
      <h1>{meet.meet.name}</h1>
      <p>{meet.meet.venue}</p>

      <div className="schedule-list">
        {activities.map((activity) => {
          const participantsInActivity = meet.entries
            .filter((entry) => entry.activityId === activity.id)
            .map((entry) => participantsById.get(entry.participantId))
            .filter(Boolean)

          return (
            <article className="activity-card" key={activity.id}>
              <h2>{activity.name}</h2>
              <p><strong>When:</strong> {formatActivityTime(activity.startTime)}</p>
              <p><strong>Location:</strong> {activity.location}</p>

              <h3>Participants</h3>
              <ul>
                {participantsInActivity.map((participant) => (
                  <li key={participant.id}>
                    {participant.name} — {participant.team}
                  </li>
                ))}
              </ul>
            </article>
          )
        })}
      </div>

      <button type="button" onClick={onBack}>
        Back to meet
      </button>
    </section>
  )
}

export default FullSchedule
