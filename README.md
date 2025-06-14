# Currency Exchange Rate Converter

## Project Overview

This project is a **Currency Exchange Rate Converter** built using **C#**, designed to convert an amount from one currency to another. It supports multiple currencies and optionally connects to live exchange rate APIs or uses fixed/local data for conversions.

---
Team Members
•	Ha Phuong Anh (A00323123)
•	Luthfat Muhammed (A00305747)
•	Lizza Martinez (A00298231)
   
   

## Features

- Convert between multiple world currencies
- Support for both current and historical exchange rates
- Console or Windows-based UI options (WinForms/WPF)
- Modular design using interfaces for extensibility
- Optional API integration for real-time exchange rates
- Input validation and user-friendly feedback

---

## Objectives

- Develop a reliable and functional currency converter
- Implement accurate currency conversion logic
- Ensure a clean and user-friendly interface
- Validate inputs and manage errors gracefully
- Support future enhancements like charts, alerts, and history

---

## Technologies Used

- **Language**: C#
- **UI Options**: Console App / WinForms / WPF
- **Data Source**: Fixed rates, local files (JSON/XML), or online APIs (e.g., Fixer.io, Trillo)

---

Application Flow
1.	The user launches the application.
2.	The user is prompted to input:
o	The amount to convert
o	Source currency (e.g., USD)
o	Target currency (e.g., EUR)
o	Optional: Specific date for historical rate
3.	The application fetches the appropriate exchange rate from:
o	Fixed local data (e.g., JSON/XML)
o	Historical data file
o	Online APIs if enabled
4.	The conversion is calculated:
o	convertedAmount = amount * exchangeRate
5.	The result is displayed to the user through the selected UI.
________________________________________
Technologies Used
•	Language: C#
•	UI Options: Console Application, WinForms, or WPF
•	Data Sources:
o	Fixed rates (JSON or XML files)
o	API data (Fixer.io, ECB)

________________________________________
Future Enhancements
•	Add exchange rate trend charts
•	Enable alert notifications for rate thresholds
•	Maintain conversion history and logs
•	Localization and multi-language support
Agile Task Board (Progress Tracker)
The following section tracks the development status of the Currency Exchange Rate Converter project. Tasks are organized into different stages to visualize project progress, support agile collaboration, and ensure accountability among team members.
________________________________________
Backlog (Not Started)
•	Add support for currency trend charts.
•	Implement alert system for exchange rate thresholds.
•	Add localization and language selection.
•	Create user manual for application usage and troubleshooting.
________________________________________
In Progress (Currently Being Worked On)
•	Final PowerPoint presentation preparation. (All)
•	Integrate software testing (unit and integration tests) into the codebase. (Luthfat)
•	UI polishing and usability improvements. (Luthfat)
________________________________________
In Review
•	API Integration: Committed real-time exchange rate fetching logic to GitHub. (Lizza)
•	Transaction History: Began development of transaction log/history feature after the first professor check-in. (Lizza)
________________________________________
Completed
•	Initial professor check-in meeting completed. (All)
•	Basic currency conversion functionality implemented. (All)
•	Input validation and error handling complete. (Luthfat)
•	Local fixed-rate JSON file integration. (Ha Phuong Anh)
•	Historical rate lookup implementation. (Lizza)
•	GitHub repository initialized and commits in place. (All)

