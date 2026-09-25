# Event Software — Project Context

**Last Updated:** September 23, 2026  
**Document Purpose:** Living source of truth for product decisions, project scope, technical direction, open questions, and learning goals.

## Status Labels

This document uses the following labels to prevent ideas from being mistaken for requirements:

- **Decided** — part of the current direction or MVP unless deliberately changed.
- **Proposed** — a likely approach that still needs design, validation, or implementation decisions.
- **Future / Stretch** — intentionally outside the core MVP and should not be added silently.

## 1. Project Overview

**Status: Decided**

Event Software is a web application intended to make large, complicated events easier to navigate. It begins with the spectator experience: helping a parent, sibling, friend, or other attendee quickly answer:

- When is the person I care about participating?
- Where do I need to be?
- What is happening now?
- What is coming up for the people I am following?

The idea grew from the difficulty of navigating events where a large master schedule contains far more information than any one spectator needs. A track meet is the initial concrete use case, but the underlying problem also appears at 4-H competitions, ballroom competitions, and similar multi-participant, multi-activity events.

The first version should turn an event's full schedule into a small, useful personal schedule for each spectator.

## 2. Core Product Vision

### Near-term vision — Decided

Make it easy for a spectator to know **where and when the people they care about are competing**.

The product should provide one source of event information while allowing each spectator to focus on selected participants. The initial experience is spectator-first because that is a valuable, testable problem small enough for the CSE 199 project.

### Long-term vision — Future / Stretch

The same event data could eventually support three related experiences:

1. **Spectators** follow participants and receive a relevant view of the schedule and event activity.
2. **Athletes or participants** receive guidance about when to prepare, warm up, or report.
3. **Organizers** manage participants, schedules, locations, delays, and live updates.

The concept may ultimately be generic enough to support multiple event types rather than being exclusively a track-meet application. That generality is a direction to preserve, not a requirement to fully solve in the MVP.

## 3. Spectator-First MVP

### MVP requirements — Decided

The minimum viable product should allow a spectator to:

1. Enter a short event or meet code.
2. Load the matching event.
3. View the event's full schedule.
4. Browse and select one or more participants to follow.
5. View a personalized schedule containing only activities involving those participants.
6. See the relevant time and location for each scheduled activity.
7. Move clearly between event lookup, the full schedule, participant selection, and the personalized schedule.

The intended basic flow is:

```text
Enter event code
        |
        v
Load event page
        |
        +----> View full schedule
        |
        v
Select participants
        |
        v
View personalized schedule
```

### MVP boundaries — Decided

- Focus on spectators, not separate athlete or organizer experiences.
- Use sample or seeded event data if needed to demonstrate the complete flow.
- Prioritize working behavior and understandable structure over visual polish.
- Produce a real full-stack prototype, not only a visual mockup.
- Do not allow future features to expand the MVP without an explicit scope decision.

## 4. Example User Story

**Status: Decided as the representative use case**

A parent arrives at a track meet to watch two athletes. The meet has many running and field events spread across several times and locations. The parent opens the web app and enters the meet's short code. The app loads the meet page and its complete schedule.

The parent selects both athletes from the participant list. The app then creates a personalized schedule containing only the events involving either athlete. Each entry shows when the event is scheduled and where it will happen. The parent can return to the full schedule when broader context is useful, but does not need to repeatedly search the entire meet schedule to find the events that matter to them.

## 5. Current Technical Direction

### Chosen direction — Decided

- **Application type:** Web app first, rather than a native mobile app.
- **Frontend:** React.
- **Language:** JavaScript.
- **Data/backend platform:** Supabase.
- **System shape:** Frontend connected to data through an appropriate backend/API boundary.
- **Version control:** Git and GitHub.
- **Development priority:** Functionality before visual polish.

### Why this direction was chosen

- A web app keeps the first version accessible and makes an MVP faster to build and test.
- React extends the developer's existing HTML, CSS, and JavaScript learning into modern component-based frontend development.
- Supabase provides database and backend capabilities without requiring every infrastructure concern to be built from scratch.
- Working across the frontend, API/data boundary, and database creates meaningful full-stack learning and portfolio experience.
- Git and GitHub build professional version-control habits and preserve the project's history.

### Important implementation detail — Open

“Backend/API” describes a desired architectural boundary, not a finalized service design. The MVP may have React communicate directly with Supabase, or it may introduce a separate backend layer where that adds enough value to justify the time. This must be decided with the 24–36 hour limit in mind.

## 6. Preliminary Architecture

**Status: Proposed; not finalized**

The current conceptual architecture is:

```text
React Web Frontend
        |
        v
Backend / API boundary
        |
        v
Supabase
        |
        +-- Events / Meets
        +-- Participants
        +-- Teams / Groups
        +-- Scheduled Activities
        +-- Locations
        +-- Participant-to-Activity Relationships
```

For an intentionally smaller MVP, the backend/API boundary may be implemented using Supabase's client and generated APIs rather than a separately deployed custom server. Authentication, authorization, security rules, and privileged organizer operations will influence whether that remains appropriate later.

Architecture should evolve from demonstrated needs. Avoid adding layers solely because a larger production system might eventually need them.

## 7. Preliminary Data Model

**Status: Proposed; entity names, fields, and relationships are not final**

### Event / Meet

Likely responsibilities:

- Unique ID
- Short lookup code
- Name
- Date or date range
- General location

### Participant / Athlete

Likely responsibilities:

- Unique ID
- Display name
- Team or group association, when relevant

### Team / Group

Likely responsibilities:

- Unique ID
- Name
- Event-specific or reusable association, to be decided

### Scheduled Activity / Scheduled Event

Likely responsibilities:

- Unique ID
- Parent event/meet
- Name or activity type
- Scheduled start time
- Location
- Optional status, later if live tracking is introduced

The term “scheduled activity” may be clearer in the data model than “event,” because the overall meet is also an event. Final naming remains open.

### Location

Likely responsibilities:

- Unique ID
- Name or label
- Optional event-specific details

For the MVP, a location might instead be stored directly on a scheduled activity if a separate table would add complexity without current value.

### Participant ↔ Scheduled Activity

A participant may enter many scheduled activities, and a scheduled activity may contain many participants. This implies a many-to-many relationship, likely represented by a join table such as `participant_activities` or `entries`.

This relationship enables the core query:

> Return all scheduled activities in this event involving the participants the spectator selected.

### Spectator selections / following

**Status: Proposed / Open**

The MVP needs a way to remember which participants are selected during use. It is not yet decided whether selections should live only in React state, persist in browser storage, or belong to an authenticated user account. Accounts are not currently an MVP requirement.

## 8. CSE 199 Scope and Constraints

**Status: Decided**

This is a CSE 199 discovery project with approximately **24–36 hours of development time**. The scope must remain deliberately small.

Success means completing a coherent learning-focused prototype with the full spectator flow. It does not mean building a production-ready platform for every kind of event.

Scope principles:

- Spectator experience first.
- Functionality over appearance.
- Learning is part of the outcome, not a delay to the outcome.
- Avoid premature abstraction and overengineering.
- Prefer a thin, complete vertical slice over many partially built features.
- Keep stretch features outside the MVP unless the core experience is already working.

## 9. Development Roadmap

**Status: Proposed working sequence**

1. Build or refine the simple React prototype and navigation flow.
2. Define the smallest database schema that supports one meet, its participants, and its schedule.
3. Create the Supabase project and tables.
4. Add realistic sample data.
5. Connect the React frontend to Supabase through the chosen data-access approach.
6. Implement event-code lookup and event loading.
7. Display the full schedule with times and locations.
8. Display participants and allow spectators to select multiple people.
9. Filter the schedule into a personalized “My Schedule” view.
10. Test the main path, empty states, invalid codes, and basic failure cases.
11. Improve usability and visual presentation only after the full flow works.
12. Document the architecture, decisions, setup, and known limitations.

The roadmap is adjustable. Each stage should produce something understandable and testable before more scope is added.

## 10. Learning and Portfolio Goals

**Status: Decided**

The project is intended both to solve a real problem and to help the developer grow beyond a classroom-sized script. Architectural choices should therefore balance delivery speed with opportunities to learn.

Primary learning goals:

- Build a component-based frontend with React.
- Strengthen JavaScript skills in a real application.
- Understand how a frontend retrieves, transforms, and presents backend data.
- Design and query a relational database.
- Learn API and backend boundaries, even if Supabase supplies much of the first implementation.
- Practice full-stack architecture and separation of responsibilities.
- Use Git and GitHub throughout development.
- Learn to control scope and make explicit engineering tradeoffs.
- Practice debugging, testing, documentation, and iterative design.

Portfolio goal:

Demonstrate a functioning full-stack prototype and be able to explain the problem, architecture, data model, tradeoffs, limitations, and potential next steps—not merely show finished screens.

## 11. Future Features and Stretch Goals

**Status: Future / Stretch; not part of the spectator MVP**

### Athlete / participant mode

- A participant-focused view of their own schedule.
- Warm-up guidance based on scheduled start time.
- Recommended report or check-in time.
- Clear directions about where to go next.

Warm-up and report guidance would eventually need rules that account for event type, organizer policy, delays, and the difference between scheduled and actual timing. It should not be presented as reliable guidance until those inputs are trustworthy.

### Organizer mode

- Create and manage events.
- Add or import participants, teams, schedules, and locations.
- Update delays, locations, and activity status.
- Publish information to spectators and participants.
- Control who may change official event data.

### Live event experience

- Live activity status, including delays and what is currently happening.
- Live scores or results.
- Notifications such as “Your athlete is about to compete.”
- Sport-specific participation signals, such as when a softball player is batting or a basketball player enters the game.
- Real-time changes reflected in relevant spectator and participant views.

### Remote viewing

The application could become valuable to people who are not physically present by combining schedule context, live status, scores, and participant-specific notifications. Remote viewing is a product direction, not part of the current MVP.

### Cross-event support

Possible event types include track meets, 4-H competitions, ballroom competitions, softball, basketball, and other structured events. Each domain has different scheduling and live-status needs. The MVP should avoid assumptions that make later expansion needlessly difficult, but it should not attempt to model every domain now.

## 12. Open Architecture and Product Questions

**Status: Open; do not treat the items below as settled decisions**

1. Should React communicate directly with Supabase for the MVP, or should the project include a separate custom backend/API?
2. What is the smallest relational schema that cleanly supports the spectator flow?
3. Should overall events and scheduled activities use different terminology in code and the UI?
4. How are short event codes generated, kept unique, validated, and expired?
5. Do spectators need accounts, or should the MVP be anonymous?
6. Where should followed-participant selections persist: component state, shared app state, browser storage, or the database?
7. Are teams/groups required for the first track-meet dataset, or can that relationship wait?
8. Should locations be their own records or simple fields in the MVP?
9. What authorization rules will protect event data, especially once organizer editing exists?
10. How should schedule ordering, simultaneous activities, cancellations, and delays be represented?
11. What real-time mechanism would support future live updates and notifications?
12. How generic should the data model be across event types before domain-specific needs justify specialization?
13. Which parts of the first implementation should be intentionally disposable prototypes, and which should establish lasting conventions?
14. What testing level provides the most learning and confidence within the time limit?

## 13. Decision Log

| Status | Decision | Reason |
|---|---|---|
| Decided | Build a web app first. | It is a practical path to an accessible MVP and avoids native-app complexity. |
| Decided | Use React and JavaScript for the frontend. | This builds on current web knowledge while teaching modern component-based development. |
| Decided | Use Supabase for database/backend capabilities. | It enables a real data-backed prototype without building all infrastructure from scratch. |
| Decided | Use Git and GitHub. | Version control is both a professional practice and a project learning goal. |
| Decided | Target spectators first. | A single audience keeps the MVP coherent and manageable. |
| Decided | Use event-code lookup followed by participant selection and schedule filtering. | This directly solves the central spectator problem. |
| Decided | Prioritize functionality over appearance. | The project has a limited 24–36 hour timeframe. |
| Decided | Treat learning and user ownership as core outcomes. | The project is educational as well as functional. |
| Proposed | Use a React → backend/API boundary → Supabase architecture. | It expresses useful separation of responsibilities, but the exact backend boundary still needs a scope-conscious decision. |
| Proposed | Model participant/activity enrollment as a many-to-many relationship. | Participants can join multiple activities and activities can contain multiple participants. |
| Future | Add athlete, organizer, live-status, scoring, notification, and remote-viewing capabilities. | These extend the product vision but would put the core school-project MVP at risk if added now. |

New decisions should be added here with their status and reasoning. If a decision changes, preserve the history and record what replaced it and why.

## 14. Assistant / Mentor Role

**Status: Decided**

ChatGPT's primary purpose in this project is to mentor the user into becoming a better software engineer, not to act as a substitute developer.

The assistant should:

- Teach the concepts and reasoning behind recommendations.
- Ask or surface questions that encourage architectural thinking.
- Explain tradeoffs, including why one solution fits the current constraints better than another.
- Help the user break problems into understandable, testable pieces.
- Guide debugging by helping form hypotheses, inspect evidence, and understand root causes.
- Review code and decisions with constructive, specific feedback.
- Connect implementation details to broader software-engineering principles.
- Gradually support greater independence and confidence.
- Protect the user's ownership of the project and ability to explain what was built.

The assistant may provide direct implementation or code when the user explicitly asks for it, or when doing so is appropriate to keep the project moving. Even then, it should explain important choices and create opportunities for the user to understand, modify, and defend the work. The default priority is learning, reasoning, and user ownership—not completing every task on the user's behalf.

## 15. Guidance for Future ChatGPT Sessions

Treat this document as the source of truth for the Event Software project until the user updates or overrides it.

Before making recommendations or changes:

1. Read this document in full.
2. Distinguish clearly among **Decided**, **Proposed**, and **Future / Stretch** items.
3. Do not silently promote brainstorming or future features into MVP requirements.
4. Optimize for both a functioning spectator-first prototype and the developer's learning goals.
5. Keep the 24–36 hour CSE 199 constraint visible in architectural and scope decisions.
6. Prefer the simplest design that teaches the relevant concept and supports the current vertical slice.
7. Explain reasoning and tradeoffs; do not merely prescribe tools or code.
8. Preserve user ownership and follow the Assistant / Mentor Role above.
9. Record important new decisions and their reasons in the Decision Log.
10. Update the **Last Updated** date whenever this document changes materially.

When information in this document conflicts with a newer explicit user decision, follow the newer decision and update this document so it remains accurate.

## 16. Current Definition of Success

The project has a successful MVP when a spectator can enter a valid event code, load an event from real persisted sample data, view its complete schedule, select one or more participants, and see a personalized schedule with the relevant times and locations—and when the developer can explain how and why the system works.
