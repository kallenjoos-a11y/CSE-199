import {useState } from 'react'

function JoinMeet({ onJoin, error}){
  const [meetCode, setMeetCode] = useState('');
  
  function handleSubmit(event) {
    event.preventDefault()
    onJoin(meetCode)
}

return (
  <section id="center">
    <form onSubmit={handleSubmit}>
          <h1>Meet Code</h1>
          <label htmlFor="text-input"></label>

          <input
            id="text-input"
            type="text"
            value={meetCode}
            onChange={(event) => setMeetCode(event.target.value)}
            placeholder="Enter Meet Code..."
          />

        <button
          type="submit"
          className="counter">
          Join
        </button>

        {error && <p role="alert">{error}</p>}
        </form>
      </section>
  )
}
export default JoinMeet
