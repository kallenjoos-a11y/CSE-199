const meetData = {
  meet: {
    id: 'meet-001',
    code: '5555',
    name: 'Fall Invitational',
    date: '2026-10-10',
    venue: 'Central High School'
  },

  participants: [
    {
      id: 'participant-001',
      name: 'Avery Smith',
      team: 'Lincoln HS'
    },
    {
      id: 'participant-002',
      name: 'Jordan Lee',
      team: 'Central HS'
    },
    {
      id: 'participant-003',
      name: 'Morgan Davis',
      team: 'Westview HS'
    },
    {
      id: 'participant-004',
      name: 'Taylor Brown',
      team: 'Lincoln HS'
    }
  ],

  activities: [
    {
      id: 'activity-001',
      name: '100m Dash',
      startTime: '2026-10-10T09:00:00-07:00',
      location: 'Track'
    },
    {
      id: 'activity-002',
      name: 'Long Jump',
      startTime: '2026-10-10T09:30:00-07:00',
      location: 'Field 1'
    },
    {
      id: 'activity-003',
      name: '400m Dash',
      startTime: '2026-10-10T10:15:00-07:00',
      location: 'Track'
    },
    {
      id: 'activity-004',
      name: 'Shot Put',
      startTime: '2026-10-10T10:30:00-07:00',
      location: 'Field 2'
    },
    {
      id: 'activity-005',
      name: '800m Run',
      startTime: '2026-10-10T11:15:00-07:00',
      location: 'Track'
    }
  ],

  entries: [
    {
      participantId: 'participant-001',
      activityId: 'activity-001'
    },
    {
      participantId: 'participant-001',
      activityId: 'activity-002'
    },
    {
      participantId: 'participant-002',
      activityId: 'activity-001'
    },
    {
      participantId: 'participant-002',
      activityId: 'activity-003'
    },
    {
      participantId: 'participant-003',
      activityId: 'activity-004'
    },
    {
      participantId: 'participant-003',
      activityId: 'activity-005'
    },
    {
      participantId: 'participant-004',
      activityId: 'activity-002'
    },
    {
      participantId: 'participant-004',
      activityId: 'activity-005'
    }
  ]
}

export default meetData
