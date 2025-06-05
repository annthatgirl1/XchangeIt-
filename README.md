# Currency Exchange Rate Converter

## Project Overview

This project is a **Currency Exchange Rate Converter** built using **C#**, designed to convert an amount from one currency to another. It supports multiple currencies and optionally connects to live exchange rate APIs or uses fixed/local data for conversions.

---

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
- **Data Source**: Fixed rates, local files (JSON/XML), or online APIs (e.g., Fixer.io, ECB)

---

## ⚙️ How It Works

1. **User inputs** the amount, source currency, target currency, and optionally a date.
2. The application **uses a rate provider** to get the exchange rate (fixed, local, or API).
3. The `CurrencyConverter` **calculates the result** by multiplying the amount by the exchange rate.
4. The result is **displayed** to the user via console or UI.
