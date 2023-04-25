# User Story - Expose new endpoint which intelligently moves work holidays to next available working days

## Description

As a user I want to make a request to the holiday service for a given year which returns both public and work holidays similar to the existing behavior, however I want all work holidays 
which fall on weekends or public holidays to be moved to the next available working day (Monday - Friday). No public holiday dates should be moved and no work holidays should occur 
on the weekend or public holiday.

## Acceptance Criteria

1. Expose a new api for this feature with the route `api/daysoff/[year]`
2. This api should return a list of public and work holidays in the provided type of `HolidayDto`
3. No public or work holidays should be removed from the list
4. If a work holiday lands on a public holiday or the weekend it needs to be moved to the next available work day (Monday - Friday)
5. The list of holidays returned should be ordered by date ascending

## Notes

- Working code is more important than clean code
- This feature is covered by a Test Driven Development approach. The existing tests will confirm if you have implemented the feature correctly without breaking existing functionality
- Do not make any changes to the tests