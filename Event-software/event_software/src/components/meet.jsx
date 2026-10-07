function Meet({ meet, onLeave, onViewFullSchedule }) {
  return (
    <section id="center">
      <h1>{meet.name}</h1>
      <p>You joined meet: {meet.code}</p>
      <p>Date: {meet.date}</p>
      <p>Location: {meet.location}</p>

      {/* <h3> Select Participant to view their schedule </h3>
      <button type="button" onClick={onViewFullSchedule}>
        Or view full schedule
      </button>
      <ul>
      {meet.participants.map(participant => (
        <li key={participant.id}>
          {participant.name} - {participant.team}
        </li>
      ))}
    </ul> */}

      <button type="button" onClick={onLeave}>
        Leave meet
      </button>
    </section>
  )
}

export default Meet
