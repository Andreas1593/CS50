-- Keep a log of any SQL queries you execute as you solve the mystery.

-- Check the crime report of the date and time the crime took place
SELECT description
FROM crime_scene_reports
WHERE month = 7 AND day = 28 AND street = "Chamberlin Street";

-- Check the witnesses' interview transcripts
SELECT name, transcript
FROM interviews
WHERE transcript LIKE "%courthouse%";

-- Check the security footage from the courthouse parking lot
SELECT license_plate, activity, hour, minute
FROM courthouse_security_logs
WHERE month = 7 AND day = 28 AND hour = 10 AND (minute >= 15 AND minute <= 30);

-- Suspects due to license plate: Patrick, Amber, Elizabeth, Roger, Danielle, Russell, Evelyn, Ernest
SELECT name, license_plate
FROM people
WHERE license_plate IN (SELECT license_plate FROM courthouse_security_logs WHERE month = 7 AND day = 28 AND hour = 10 AND (minute >= 15 AND minute <= 30));

-- Check transaction via the ATM on Fifer Street that day
SELECT account_number, transaction_type, amount
FROM atm_transactions
WHERE month = 7 AND day = 28 AND atm_location = "Fifer Street" AND transaction_type = "withdraw";

-- Suspects due to account number: Ernest, Russell, Roy, Bobby, Elizabeth, Danielle, Madison, Victoria
SELECT name, account_number
FROM people
JOIN bank_accounts ON bank_accounts.person_id = people.id
WHERE account_number IN (SELECT account_number FROM atm_transactions WHERE month = 7 AND day = 28 AND atm_location = "Fifer Street" AND transaction_type = "withdraw");

-- Suspects left: Elizabeth, Danielle, Russell, Ernest

-- Check the earliest flight out of Fiftyville the day after the crime
SELECT full_name, city, hour, minute, flights.id
FROM airports
JOIN flights ON flights.destination_airport_id = airports.id
WHERE month = 7 AND day = 29 AND origin_airport_id = (SELECT id FROM airports WHERE city = "Fiftyville")
ORDER BY hour, minute
LIMIT 1;

-- Check passengers on that flight: Bobby, Roger, Madison, Danielle, Evelyn, Edward, Ernest, Doris
SELECT name
FROM people
WHERE passport_number IN (SELECT passport_number FROM passengers WHERE flight_id IN (SELECT flights.id FROM airports JOIN flights ON flights.destination_airport_id = airports.id
    WHERE month = 7 AND day = 29 AND origin_airport_id = (SELECT id FROM airports WHERE city = "Fiftyville") ORDER BY hour, minute LIMIT 1));

-- Supects left: Danielle, Ernest

-- Check phone calls that day which took less than a minute: Anna, Berthold, Bobby, Doris, Ernest, Evelyn, Jack, Jacqueline, James, Kimberly, Larry, Madison, Melissa, Philip, Roger, Russel, Victoria
SELECT caller, receiver, duration, name
FROM phone_calls, people
WHERE month = 7 AND day = 28 AND duration < 60 AND (people.phone_number = caller OR people.phone_number = receiver)
GROUP BY name;

-- Suspects left: Ernest

-- Ernest's phone number
SELECT name, phone_number FROM people WHERE name = "Ernest";

-- Check Ernest's phone calls that day
SELECT caller, receiver, duration, name
FROM phone_calls, people
WHERE month = 7 AND day = 28 AND name = "Ernest" AND (caller = (SELECT phone_number FROM people WHERE name = "Ernest") OR receiver = (SELECT phone_number FROM people WHERE name = "Ernest"));

-- Check who Ernest was phoning with that day for less than a minute: Berthold
SELECT name AS accomplice, phone_number
FROM people
WHERE phone_number IN (SELECT receiver FROM phone_calls, people WHERE month = 7 AND day = 28 AND name = "Ernest"
    AND (caller = (SELECT phone_number FROM people WHERE name = "Ernest") OR receiver = (SELECT phone_number FROM people WHERE name = "Ernest")) AND duration < 60);