import { useState } from 'react'
import './App.css'
import JoinMeet from './components/joinMeet'
import Meet from './components/meet'

function App() {
  const [activeMeet, setActiveMeet] = useState(null);
  const [error, setError] = useState('');

  const validMeetCode = '5555';

  function handleJoin(submittedCode) {
    const normalizedCode = submittedCode.trim()

    if (normalizedCode !== validMeetCode){
      setError('Invalid code. Try again.')
      return
    }

    setError('')
    setActiveMeet({
      code: normalizedCode,
    })
  }

  function handleLeaveMeet(){
    setActiveMeet(null)
  }

  if (activeMeet === null) {
    return(
      <JoinMeet
        onJoin={handleJoin}
        error={error}
      />
    )
  }

  return (
    <Meet 
      meet={activeMeet}
      onLeave={handleLeaveMeet}
    />
  )
}

export default App