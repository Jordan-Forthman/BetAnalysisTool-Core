Project Overview: Cross-Platform Sports Betting Analysis Tool

Team Members: Jordan Forthman, Grace Hutchinson, Miguel Alvarez Machado, Rodner Vincent, Quin Mosely, Ivan Liuliaev


Objectives:
----------------------------------------------------------------------------------------------------------------------------

Real-Time Information Delivery: Provide live news, odds, and updates from reliable sources (e.g., ESPN or Odds API), enabling users to react swiftly—targeting <5-second latency for pushes, based on SignalR benchmarks.

Statistical Aggregation and Visualization: Compile player/team stats (e.g., offensive/defensive rankings) and display trends via interactive charts, helping users identify patterns.

Matchup Analysis: Offer comparative insights on teams/players across categories (e.g., points per game, injury impacts), supporting informed decision-making.

Subscription Model: Implement tiered access (free basic vs. premium features like advanced analytics), with user authentication to drive retention.

Cross-Platform Accessibility: Deploy on iOS, Android, ensuring seamless offline support for cached data.

Technology Stack: 
----------------------------------------------------------------------------------------------------------------------------

Frontend: .NET MAUI with CommunityToolkit.Mvvm for iOS/Android, Blazor WebAssembly for web extension post-MVP. 

Backend: ASP.NET Core Web API for RESTful services, SignalR for real-time websockets, hosted on Azure App Service for scalability. 

Data Layer: SQLite for local/offline storage, Azure SQL Database for cloud-shared/subscription data. 

Security/Auth: Auth0 for user logins and RBAC, ensuring compliance with data privacy standards. 

Visualization: Syncfusion (MAUI) and Chart.js (web) for charts/trends. 

Deployment/Testing: Azure for hosting, TestFlight/Google Play for mobile betas.

