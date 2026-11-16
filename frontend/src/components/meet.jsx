function Meet({ meet, onLeave, onViewFullSchedule }) {
  return (
    <section id="center">
      <h1>{meet.meet.name}</h1>
      <p>You joined meet: {meet.meet.code}</p>
      <p>Date: {meet.meet.date}</p>
      <p>Location: {meet.meet.venue}</p>

      <button type="button" onClick={onViewFullSchedule}>
        Full Schedule
      </button>

      <button type="button" onClick={onLeave}>
        Leave meet
      </button>
    </section>
  )
}

export default Meet
