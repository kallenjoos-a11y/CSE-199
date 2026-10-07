import { useState } from 'react'
import './App.css'
import JoinMeet from './components/joinMeet'
import Meet from './components/meet'
import FullSchedule from './components/fullSchedule'

function App() {
  const [activeMeet, setActiveMeet] = useState(null);
  const [activeView, setActiveView] = useState('meet')
  const [error, setError] = useState('');

  async function handleJoin(submittedCode) {
    const normalizedCode = submittedCode.trim()
    setError('')

    if (!normalizedCode) {
      setError('Enter a meet code.')
      return
    }

    try {
      const response = await fetch(
        `http://localhost:5069/meets/${encodeURIComponent(normalizedCode)}`
      )

      if (!response.ok) {
        throw new Error(`API returned status ${response.status}`)
      }

      const meet = await response.json()
      setActiveMeet(meet)
      setActiveView('meet')
    } catch (err) {
      console.error('Joining meet failed:', err)
      setError('Could not load the meet. Check that the API is running.')
    }
  }

  function handleLeaveMeet(){
    setActiveMeet(null)
    setActiveView('meet')
  }

  function handleViewFullSchedule(){
    setActiveView('fullSchedule')
  }

  if (activeMeet === null) {
    return(
      <JoinMeet
        onJoin={handleJoin}
        error={error}
      />
    )
  }

  if (activeView === 'fullSchedule') {
    return (
      <FullSchedule
        meet={activeMeet}
        onBack={() => setActiveView('meet')}
      />
    )
  }

  return (
    <Meet 
      meet={activeMeet}
      onLeave={handleLeaveMeet}
      onViewFullSchedule={handleViewFullSchedule}
    />
  )
}

export default App
