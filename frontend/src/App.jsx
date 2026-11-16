import { useEffect, useState } from 'react'
import './App.css'
import JoinMeet from './components/joinMeet'
import Meet from './components/meet'
import FullSchedule from './components/fullSchedule'
import meetData from './data/meetData'
import Header from './components/eventHeader.jsx'

const ACTIVE_MEET_STORAGE_KEY = 'event-software.active-meet'

function App() {
  const [activeMeet, setActiveMeet] = useState(() => {
    const savedCode = localStorage.getItem(ACTIVE_MEET_STORAGE_KEY)

    // Remember the meet, but always load its current mock data.
    return savedCode === meetData.meet.code ? meetData : null
  })
  const [activeView, setActiveView] = useState('meet')
  const [error, setError] = useState('');

  useEffect(() => {
    if (activeMeet === null) {
      localStorage.removeItem(ACTIVE_MEET_STORAGE_KEY)
      return
    }

    localStorage.setItem(ACTIVE_MEET_STORAGE_KEY, activeMeet.meet.code)
  }, [activeMeet])

  function handleJoin(submittedCode) {
    const normalizedCode = submittedCode.trim()
    setError('')

    if (!normalizedCode) {
      setError('Enter a meet code.')
      return
    }

    if (normalizedCode !== meetData.meet.code) {
      setError('No meet found with that code.')
      return
    }

    setActiveMeet(meetData)
    setActiveView('meet')
  }

  function handleLeaveMeet(){
    setActiveMeet(null)
    setActiveView('meet')
  }

  return (
    <div>
      <Header />
      {activeMeet === null ? (
        <JoinMeet
          onJoin={handleJoin}
          error={error}
        />
      ) : activeView === 'fullSchedule' ? (
        <FullSchedule
          meet={activeMeet}
          onBack={() => setActiveView('meet')}
        />
      ) : (
        <Meet
          meet={activeMeet}
          onLeave={handleLeaveMeet}
          onViewFullSchedule={() => setActiveView('fullSchedule')}
        />
      )}
    </div>
  )
}

export default App
