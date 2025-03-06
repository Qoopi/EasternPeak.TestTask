# Technical Test Assignment (3 Hours)

## Tested Websites

1. Kitchen Applitools: https://kitchen.applitools.com/
2. ReqRes API: https://reqres.in/

## Objective

Develop automated UI and API tests to verify the functionality of Kitchen Applitools and ReqRes
using C#, Selenium, and RestSharp.

## 1. UI Testing (Selenium, C#)

Automate a test to verify the Drag & Drop functionality of Kitchen Applitools using Selenium
WebDriver.

### Test Case: Verify Parameterized Drag & Drop Functionality

1. Open the Kitchen Applitools homepage.
2. Navigate to the 'Drag and Drop' section.
3. Drag a parameterized menu item (e.g., 'Pizza', 'Sushi', 'Burger') from 'Menu' to the 'Order Ticket' area.
4. Verify that after dropping, the text inside the 'Order Ticket' matches the dragged item.
5. Perform a visual validation of the drop area after the drag-and-drop action.
6. Close the browser.

### Additional Requirements

- The test must be parameterized to support different menu items.
- Use Actions Class in Selenium for simulating Drag & Drop.
- Implement the test using the Page Object Model (POM).
- Use JavaScript Executor if the standard Drag & Drop method does not work.
- Include logging for each test step.

## 2. API Testing (RestSharp, C#)

Automate API tests for ReqRes using RestSharp.

### Test Cases

1. **Verify Fetching User List**
   - Perform GET request to /api/users?page=2.
   - Validate response status code 200 OK.
   - Ensure at least one user exists.
   - Verify the first user's id is not empty.

2. **Verify Creating a New User**
   - Perform POST request to /api/users with name and job data.
   - Ensure response contains id, name, and job.
   - Validate response status code 201 Created.

## Code Requirements

1. Clean, well-structured code with comments.
2. All tests must be independent of each other.
3. Logging: Include logs for all API requests and responses.
4. Project structure should follow SOLID principles.

## Deadline

Estimated completion time: 3 hours.

## Deliverables

Upon completion, provide:

- A GitHub repository or a ZIP archive with the project.
- A README.md file with instructions on how to run the tests.
