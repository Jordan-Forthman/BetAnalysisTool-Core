Project Overview: Cross-Platform Sports Betting Analysis Tool
Project Title: BetAnalysisTool
Team Composition: 5 computer science students with experience in C#, .NET, SQL/SQLite, data structures/algorithms (C++), operating systems (C), and secure/parallel/distributed development (Python). 
Duration: One semester.
Methodology: Agile Scrum with 2-week sprints, incorporating daily standups, sprint planning/reviews/retros, and tools like Azure DevOps for backlog management to ensure iterative progress and adaptability.
Project Purpose and Scope:
This capstone project aims to develop a subscription-based, cross-platform application that empowers users to make informed sports betting decisions through data-driven insights. By aggregating real-time sports data, visualizing trends, and providing analytical tools, the app addresses the growing demand for accessible betting analytics—market research indicates the global sports betting industry will reach $182B by 2030 (Statista, 2023 projections adjusted for growth). The scope focuses on foundational features for MVP delivery within the semester, with scalability for future expansions like AI predictions.

Objectives:
1.	Real-Time Information Delivery: Provide live news, odds, and updates from reliable sources (e.g., ESPN or Odds API), enabling users to react swiftly—targeting <5-second latency for pushes, based on SignalR benchmarks.
2.	Statistical Aggregation and Visualization: Compile player/team stats (e.g., offensive/defensive rankings) and display trends via interactive charts, helping users identify patterns.
3.	Matchup Analysis: Offer comparative insights on teams/players across categories (e.g., points per game, injury impacts), supporting informed decision-making.
4.	Subscription Model: Implement tiered access (free basic vs. premium features like advanced analytics), with user authentication to drive retention—projected 20-30% conversion rate based on similar apps (e.g., DraftKings analytics tools).
5.	Cross-Platform Accessibility: Deploy on iOS, Android, and web for broad reach, ensuring seamless offline support for cached data.
6.	Educational Outcomes: Demonstrate full-stack skills, including secure networking (RBAC from Python course) and efficient data handling (from algorithms/OS courses), culminating in a production-ready prototype.

Technology Stack (Validated for Effectiveness):
	Frontend: .NET MAUI with CommunityToolkit.Mvvm for iOS/Android (single codebase, reducing dev time by 40% per Microsoft studies); Blazor WebAssembly for web extension post-MVP.
	Backend: ASP.NET Core Web API for RESTful services; SignalR for real-time websockets; hosted on Azure App Service for scalability.
	Data Layer: SQLite for local/offline storage; Azure SQL Database for cloud-shared/subscription data.
	Security/Auth: Auth0 for user logins and RBAC, ensuring compliance with data privacy standards.
	Visualization: Syncfusion (MAUI) and Chart.js (web) for charts/trends.
	Deployment/Testing: Azure for hosting; TestFlight/Google Play for mobile betas.

Development Phases (High-Level Timeline):
	Weeks 1-2 (Sprint 1): Setup foundation—project scaffolding, Auth0 integration, SQLite schema, and basic API prototype. Validate with spike on data sources.
	Weeks 3-6 (Sprints 2-3): Core features—real-time news/odds display, stat aggregation, and initial visualizations. Implement offline caching and basic UI.
	Weeks 7-10 (Sprints 4-5): Advanced analysis—matchup tools, trend charts, and subscription gating. Integrate SignalR for live updates.
	Weeks 11-12 (Sprint 6): Web extension via Blazor; full integration testing; user auth refinements.
	Weeks 13-16: Polish, deployment, and documentation. Conduct beta testing with 10-20 peers; finalize app store submissions.

Data Sources and Integration:
	Fetch from free/public APIs (e.g., SportsData.io or TheSportsDB) for stats/news; implement caching to handle rate limits (e.g., 100 calls/minute thresholds).
	Use ETL processes in backend for aggregation, ensuring data freshness via scheduled jobs.

Quality Assurance:
	Unit/integration tests with xUnit; CI/CD via Azure Pipelines for automated builds (aiming for 80% code coverage).
	Security scans for vulnerabilities (e.g., OWASP top 10); performance testing for mobile battery/CPU impact.

