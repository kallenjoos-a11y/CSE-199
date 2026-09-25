function Meet({ meet, onLeave }) {
  return (
    <section id="center">
      <h1>Meet Loaded</h1>
      <p>You joined meet: {meet.code}</p>

      <button type="button" onClick={onLeave}>
        Leave meet
      </button>
    </section>
  )
}

export default Meet