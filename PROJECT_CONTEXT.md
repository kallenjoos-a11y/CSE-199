# Event Software — Project Context

**Last Updated:** November 16, 2026
**Document Purpose:** Living source of truth for product decisions, project scope, technical direction, open questions, and learning goals.

## Start Here Next Time

**Current milestone — Approved October 21, 2026 (revised):** Apply the approved BeThere design to the viewer interface, then make participant names clickable to open individual schedules. Continue using mock data.

**Next action:** Propose the first responsive viewer UI change for approval: the BeThere header, event banner, and desktop/mobile navigation using the approved navy/coral styling. Explain component responsibilities before implementing.

**Deferred:** Image/PDF import, AI extraction, and organizer tools. Revisit these after the viewer experience; there is no active import milestone.

**Last completed — October 16, 2026:** Mock meet lookup with code `5555`, meet details, Full Schedule / Back to meet navigation, refresh restoration, and Leave meet. Empty and unknown codes show errors. Build, lint, and the main browser flows passed then; these are historical checks, not a new test run.

**Current implementation:** React + JavaScript + Vite + plain CSS. `src/data/meetData.js` supplies `{ meet, participants, activities, entries }`. `App.jsx` loads that dataset into React state and stores only the joined code under `event-software.active-meet` in browser localStorage. No imported-meet persistence or AI extraction exists yet. A separate .NET project exists at `../backend/BeThere`; the frontend currently uses mock data instead of fetching from it.

**Working agreement:** Propose a manageable change, let the user approve or modify it, then implement, explain, and verify it. Ask before major architectural changes. Keep this status and the roadmap current as milestones are completed.

## Approved Visual Direction

**Status: Decided; current implementation priority.**

- Product name: **BeThere**.
- Desktop: My People on the left and a combined chronological schedule of followed participants on the right.
- Mobile: two primary bottom tabs, My People and Full Schedule.
- Navy/coral base styling, an event banner, and customizable organizer branding.
- Consistent participant avatar colors from a coordinated soft palette, with visible names.
- Individual participant schedules and individual/group share-link UI.
- Shared views should resolve participant IDs against current schedule data. Real updates across devices require shared persisted data and are not provided by the current mock prototype.

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

- Focus current work on the viewer/spectator experience. Image/PDF import, AI extraction, organizer tools, and athlete mode are deferred.
- Use sample or seeded event data if needed to demonstrate the complete flow.
- The first schedule version supports individual participant activities only.
- Relays, heats, flights, lanes, results, live updates, and complex competition rounds are outside the initial MVP.
- The data model should leave room for these features later without requiring them now.
- Preserve working behavior and understandable structure while applying the approved viewer design first, followed by clickable individual participant schedules.
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
- **Data/backend platform:** Use the existing ASP.NET Core/.NET API as the backend, with real persistent storage planned. The persistence provider remains undecided. Continue using mock data during the current viewer milestone.
- **System shape:** Frontend connected to data through an appropriate backend/API boundary.
- **Version control:** Git and GitHub.
- **Development priority:** Apply the approved viewer design, then add clickable participant schedules using mock data. Preserve the existing join/navigation flow.

### Why this direction was chosen

- A web app keeps the first version accessible and makes an MVP faster to build and test.
- React extends the developer's existing HTML, CSS, and JavaScript learning into modern component-based frontend development.
- The .NET API provides a clear backend boundary and creates a meaningful full-stack learning opportunity; the persistence provider will be selected separately.
- Working across the frontend, API/data boundary, and database creates meaningful full-stack learning and portfolio experience.
- Git and GitHub build professional version-control habits and preserve the project's history.

### Important implementation detail — Open

The current frontend reads local mock data. The existing .NET API at `../backend/BeThere` is now the chosen backend direction, but it is not yet integrated with the frontend and does not yet provide the planned persistent event data. Backend integration is not required for the current viewer milestone. The persistence provider, database schema, API contract, and deployment approach remain open and must be agreed on before backend implementation. If AI importing is resumed later, use the backend for server-side AI access and storage with user approval. AI credentials must stay on the server. No provider or model has been selected. Architecture should evolve from demonstrated needs and the 24–36 hour limit; avoid adding layers solely because a larger production system might eventually need them.

**Development launcher robustness — Proposed / Future:** If it remains relevant, make `scripts/start-dev.sh` resolve the repository root from the script's own location so it can be launched from outside the repository root. This is a development-tooling improvement, not part of the current viewer milestone.

**README alignment — Future:** Update the repository `README.md` to accurately describe the current BeThere project, development setup, architecture direction, and available commands. This is documentation follow-up work, not part of the current viewer milestone.

## 6. Preliminary Architecture

**Status: Proposed; backend direction decided, persistence details not finalized**

The current conceptual architecture is:

```text
React Web Frontend
        |
        v
ASP.NET Core/.NET API
        |
        v
Persistent storage provider (not yet selected)
        |
        +-- Events / Meets
        +-- Participants
        +-- Teams / Groups
        +-- Scheduled Activities
        +-- Locations
        +-- Participant-to-Activity Relationships
```

The .NET API is the chosen backend boundary. Authentication, authorization, security rules, deployment, and privileged organizer operations remain future design considerations.

## 7. Initial Data Model

**Status: Decided for the initial implementation**

The first implementation will use a small normalized data model that supports the spectator experience while remaining expandable.

### Meet

A meet is the overall scheduled event.

Likely fields:

- `id`
- `code`
- `name`
- `date`
- `venue`

### Participant

A participant is an individual athlete competing in the meet.

Likely fields:

- `id`
- `meet_id`
- `name`
- `team`

### Activity

An activity is one scheduled individual event, such as the 100m Dash or Long Jump.

Likely fields:

- `id`
- `meet_id`
- `name`
- `start_time`
- `location`

The initial version will use one start time and one text location for each activity.

### Entry

An entry connects a participant to an activity.

Likely fields:

- `participant_id`
- `activity_id`

This relationship supports the core use case:

- One participant may participate in multiple activities.
- One activity may include multiple participants.
- A spectator may follow multiple participants.
- The application can filter activities through participant entries.

The initial implementation will support individual events only. Relay teams, heats, flights, lanes, results, and live status information are future extensions.

### Spectator selections / following

**Status: Proposed / Open**

Until API integration exists, followed-participant selections should live only in React state for the current session. Do not add browser-storage persistence for followed participants. Accounts and server-backed following are not currently part of the viewer milestone.

## 8. Organizer Schedule Input

### Image/PDF import — Deferred idea

Previously approved as the next milestone, then explicitly deferred on October 21, 2026 to restore viewer-first focus. Preserve the idea for later; the following describes a possible future approach, not an active checklist.

An organizer could select one schedule image or PDF, review an editable draft, correct missing or ambiguous information, and confirm the import to open the meet in BeThere.

If resumed and approved, build this in two stages:

1. **UI prototype:** File selection and an editable review table using a clearly labeled sample extraction result. Confirmation loads the reviewed data into the existing meet/schedule views for the current session. Do not imply the sample was extracted from the selected file. A refresh need not preserve this temporary imported meet; make the limitation visible.
2. **Real extraction and storage:** Review the existing .NET backend and agree on an AI provider, supported file limits, validation, code generation, and persistence. Perform extraction through the backend with server-held credentials. Store a confirmed meet so its code resolves to its saved data after refresh. Saving only a code is insufficient until that lookup exists.

The normalized output must match the current frontend shape: `{ meet, participants, activities, entries }`, including `startTime`, `participantId`, and `activityId` in JavaScript. Drafts may contain missing values, but confirmation must validate the required fields and references before loading the existing schedule screens.

Extract meet details, activity names, times, and locations; include participants and their entries only when supplied by the source. Do not invent missing people, enrollment relationships, dates, or times. Let the organizer review the source alongside the draft and correct uncertain values. Resolve the event date and time zone before generating complete activity timestamps. A schedule-only source may produce an empty participant list and must still support viewing activities.

### Later import options — Proposed / Future

- All import work is deferred. Webpage URL import could follow image/PDF import if that work is resumed.
- CSV/spreadsheet import remains a possible structured alternative; it is not the next milestone.
- Full organizer management, automatic publication, natural-language schedule updates, and delay recalculation remain future work.

## 9. Development Seed Data

**Status: Implemented for the mock prototype**

`src/data/meetData.js` contains the Fall Invitational (code `5555`), four participants, five activities, and their entries. The current frontend uses this file directly.

Before connecting the application to the .NET API and persistent storage, the project will use a JavaScript seed data file containing one realistic mock event.

The seed data should use the same conceptual structure as the planned database:

- One meet
- Multiple individual participants
- Multiple activities
- Participant-to-activity entries
- Activity start times and locations

The seed file will allow the React application to demonstrate the full spectator flow without requiring database setup immediately.

The seed data should be structured so that it can later be replaced by .NET API responses without requiring major changes to the schedule components.

## 10. CSE 199 Scope and Constraints

**Status: Decided**

This is a CSE 199 discovery project with approximately **24–36 hours of development time**. The scope must remain deliberately small.

Success means completing a coherent learning-focused prototype with the full spectator flow. It does not mean building a production-ready platform for every kind of event.

Scope principles:

- Spectator experience first.
- Current sequence: approved viewer appearance first, then clickable individual schedules; preserve existing functionality throughout.
- Learning is part of the outcome, not a delay to the outcome.
- Avoid premature abstraction and overengineering.
- Prefer a thin, complete vertical slice over many partially built features.
- Keep stretch features outside the MVP unless the core experience is already working.

## 11. Development Roadmap

**Priority approved October 21, 2026 (revised):** Focus on the viewer. Apply the approved design first, then make participant names clickable to view their schedules. Continue with mock data. Propose each manageable implementation change for user approval.

### Completed foundation

- [x] Define the mock meet, participant, activity, and entry structure.
- [x] Join mock meet `5555`, show details and the chronological full schedule, return, and leave.
- [x] Remember only the joined meet code and resolve it against current mock data on refresh.
- [x] Check empty/invalid codes, navigation, refresh, and leaving; build and lint passed October 16.

### Active viewer checklist

- [ ] **1. Responsive visual frame:** BeThere header, event banner, navy/coral styling, desktop navigation, and mobile My People / Full Schedule tabs.
- [ ] **2. Dashboard appearance:** My People on the left, combined upcoming events on the right, coordinated participant avatars, and consistent reusable schedule cards. Use mock participants while interactive following is still pending.
- [ ] **3. Clickable participant names:** Open the selected person's schedule, showing name, team, activity times, locations, a clear way back, and an empty state when they have no activities. Derive the schedule from existing entries rather than copying schedule data.
- [ ] **4. Search and following:** Add participant search and follow/unfollow; update the combined schedule based on selected participant IDs.
- [ ] **5. Sharing and themes:** Add visual individual/group sharing UI and organizer branding customization after the core viewer screens. Functional cross-device links are outside the current MVP; organizer management remains deferred.
- [ ] **6. Viewer review:** Verify mobile layout, keyboard access, empty states, and the complete join / dashboard / participant schedule / full schedule / leave flow; run build and lint for relevant code changes.

### Deferred work — revisit after the viewer experience

- Image/PDF import, a sample extraction draft, editable review, and confirmation.
- Real AI extraction, provider selection, and persisted imported meets.
- Webpage/CSV imports and broader organizer tools.
- Delay recalculation, AI interpretation of updates, notifications, and results.
- Backend integration and real persistence remain after the current mock-data viewer milestone. The .NET API direction is decided; the persistence provider, schema, and API contract remain open.

## 12. Learning and Portfolio Goals

**Status: Decided**

The project is intended both to solve a real problem and to help the developer grow beyond a classroom-sized script. Architectural choices should therefore balance delivery speed with opportunities to learn.

Primary learning goals:

- Build a component-based frontend with React.
- Strengthen JavaScript skills in a real application.
- Understand how a frontend retrieves, transforms, and presents backend data.
- Design and query a relational database.
- Learn API and backend boundaries, including how the .NET API connects the frontend to persistent data.
- Practice full-stack architecture and separation of responsibilities.
- Use Git and GitHub throughout development.
- Learn to control scope and make explicit engineering tradeoffs.
- Practice debugging, testing, documentation, and iterative design.

Portfolio goal:

Demonstrate a functioning full-stack prototype and be able to explain the problem, architecture, data model, tradeoffs, limitations, and potential next steps—not merely show finished screens.

## 13. Future Features and Stretch Goals

**Status: Future / Stretch; not part of the spectator MVP**

### Athlete / participant mode

- A participant-focused view of their own schedule.
- Warm-up guidance based on scheduled start time.
- Recommended report or check-in time.
- Clear directions about where to go next.

Warm-up and report guidance would eventually need rules that account for event type, organizer policy, delays, and the difference between scheduled and actual timing. It should not be presented as reliable guidance until those inputs are trustworthy.

### Organizer mode

All organizer tools, including the previously discussed image/PDF import, are deferred while the viewer experience is built:

- Manage existing events and organizer access.
- Manage participants, teams, schedules, and locations beyond the import flow.
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

## 14. Open Architecture and Product Questions

**Status: Open; do not treat the items below as settled decisions**

1. Which persistence provider, database schema, and API contract should the .NET backend use? These are not prerequisites for the current viewer milestone.
2. What is the smallest relational schema that cleanly supports the spectator flow?
3. Legacy code may continue using “meet” temporarily, but user-facing product terminology should use “event.”
4. How are short event codes generated, kept unique, validated, and expired?
5. Do spectators need accounts, or should the MVP be anonymous?
6. How should followed-participant selections be modeled when API-backed persistence is added after the viewer milestone?
7. Are teams/groups required for the first track-meet dataset, or can that relationship wait?
8. Should locations be their own records or simple fields in the MVP?
9. What authorization rules will protect event data, especially once organizer editing exists?
10. How should schedule ordering, simultaneous activities, cancellations, and delays be represented?
11. What real-time mechanism would support future live updates and notifications?
12. How generic should the data model be across event types before domain-specific needs justify specialization?
13. Which parts of the first implementation should be intentionally disposable prototypes, and which should establish lasting conventions?
14. What testing level provides the most learning and confidence within the time limit?

## 15. Decision Log

| Status | Decision | Reason |
|---|---|---|
| Decided | Build a web app first. | It is a practical path to an accessible MVP and avoids native-app complexity. |
| Decided | Use React and JavaScript for the frontend. | This builds on current web knowledge while teaching modern component-based development. |
| Historical decision; superseded November 16, 2026 | Use Supabase for database/backend capabilities. | Original direction; the project now chooses the existing .NET API as its backend. |
| Decided | Use Git and GitHub. | Version control is both a professional practice and a project learning goal. |
| Decided | Target spectators first. | A single audience keeps the MVP coherent and manageable. |
| Decided | Use event-code lookup followed by participant selection and schedule filtering. | This directly solves the central spectator problem. |
| Historical priority; revised October 21, 2026 | Prioritize functionality over appearance. | The current user-approved sequence applies the viewer design first, then clickable participant schedules, while preserving working behavior. |
| Decided | Treat learning and user ownership as core outcomes. | The project is educational as well as functional. |
| Superseded — November 16, 2026 | Use a React → backend/API boundary → Supabase architecture. | Supabase is no longer the active primary backend direction. |
| Decided — November 16, 2026 | Use the existing ASP.NET Core/.NET API as the backend and add real persistence later. | Establishes the backend direction while leaving the persistence provider, schema, and API contract open for a deliberate follow-up decision. |
| Proposed | Model participant/activity enrollment as a many-to-many relationship. | Participants can join multiple activities and activities can contain multiple participants. |
| Future / Deferred | Add athlete, organizer, live-status, scoring, notification, and remote-viewing capabilities. | Viewer-first focus restored October 21; image/PDF import is also deferred. |
| Decided — October 16, 2026 | Use mock data for the current frontend; persist only the joined meet code. | Keep the prototype independent of the API and reload current mock data after refresh. |
| Decided — October 2026; sharing scope clarified November 16, 2026 | Preserve the approved responsive BeThere design. | Two-column desktop dashboard, two mobile tabs, coordinated avatars, organizer branding, and visual sharing UI. Functional cross-device sharing is not part of the current MVP. |
| Superseded — October 21, 2026 | Prioritize image/PDF import, editable review, and confirmation before styling. | Initially approved to simplify meet entry; later deferred by the viewer-first decision below. |
| Deferred — October 21, 2026 | Choose the AI provider, backend integration, and persistent import storage. | Revisit when import work resumes; inspect existing .NET work and get approval before major architectural changes. |
| Decided — October 21, 2026 (revised) | Apply the approved viewer design first, then make participant names clickable to open individual schedules. | Restore viewer-first focus, keep mock data, and defer AI importing and organizer tools. |
| Decided — November 16, 2026 | Keep followed-participant selections in session-only React state until API integration exists. | Avoid adding browser persistence that will be replaced by the API-backed approach. |
| Decided — November 16, 2026 | Use “event” as the user-facing product term. | Keep the product language general while allowing legacy “meet” code to transition later. |

New decisions should be added here with their status and reasoning. If a decision changes, preserve the history and record what replaced it and why.

## 16. Assistant / Mentor Role

**Status: Decided**

ChatGPT's primary purpose in this project is to mentor the user into becoming a better software engineer, not to act as a substitute developer.

The assistant should:

- Propose each manageable change for the user to approve or modify before implementation; explain and verify the result afterward. Ask before major architectural changes.
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

## 17. Guidance for Future ChatGPT Sessions

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

## 18. Current Definition of Success

**Next milestone success:** The viewer interface matches the approved responsive BeThere design: navy/coral styling, event banner, two-column desktop dashboard, two mobile tabs, coordinated participant avatars, and consistent schedule cards. Selecting a participant name opens that person's schedule with name, team, times, locations, and a clear route back. Use existing mock data; imports and organizer tools are deferred.

**Overall MVP success:** The project has a successful MVP when a spectator can enter a valid event code, load an event through the .NET API from real persisted sample data, view its complete schedule, select one or more participants for the current session, and see a personalized schedule with the relevant times and locations—and when the developer can explain how and why the system works. Functional cross-device sharing is not required for this MVP.
