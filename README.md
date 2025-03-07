# Eastern Peak Test Task: Kitchen Applitools & ReqRes 

This project contains automated UI and API tests for:

1. **[Kitchen Applitools](https://kitchen.applitools.com/)** - UI testing with Selenium WebDriver
2. **[ReqRes API](https://reqres.in/)** - API testing with RestSharp

This implementation is based on the [technical test assignment](./test_task.md) requirements.

## Technology Stack

- **C# 12** with .NET 9.0
- **NUnit** as the test framework
- **Selenium WebDriver** for UI testing
- **RestSharp** for API testing
- **Serilog** for logging
- **Verify.ImageSharp.Compare** for visual validation

## Project Structure

```
EasternPeak.TestTask/
├── tests/
│   ├── Api.ReqRes/                # API tests for ReqRes
│   │   ├── Clients/               # API client implementations
│   │   │   ├── ReqResApiClient.cs # API client with interface
│   │   │   └── TestEndpoints.cs   # Centralized API endpoints
│   │   ├── Models/                # Data models for API testing
│   │   ├── TestBase.cs            # Base test class with common setup
│   │   └── ReqResTests.cs         # API test cases
│   │
│   └── Ui.KitchenApplitools/      # UI tests for Kitchen Applitools
│       ├── Pages/                 # Page object classes
│       │   ├── BasePage.cs        # Base page with common methods
│       │   └── KitchenPage.cs     # Kitchen page implementation
│       ├── TestUtils/             # Test utilities
│       │   ├── Helpers/           # Helper methods
│       │   ├── Screenshots/       # Screenshot utilities
│       │   ├── Scripts/           # JavaScript files for drag-and-drop
│       │   └── WebDriver/         # WebDriver factory and configuration
│       ├── Resources/             # Test resources and verified screenshots
│       ├── TestBase.cs            # Base test class with common setup
│       └── DragDropTests.cs       # UI test cases
│
├── test_task.md                   # Original test assignment requirements
└── EasternPeak.TestTask.sln       # Solution file
```

## Requirements

- **.NET 9.0 SDK**
- **Chrome,FireFox, or Edge browser** (for UI tests)
- **Internet connection** (to access Kitchen Applitools and ReqRes API)

## How to Run the Tests

To run test cases, navigate to the project root directory and execute the following commands:

### Build the Solution

```bash
dotnet build
```

### Run All Tests

```bash
dotnet test
```

### Run API Tests Only

```bash
dotnet test tests/Api.ReqRes
```

### Run UI Tests Only

```bash
dotnet test tests/Ui.KitchenApplitools
```

### Run Specific Test

```bash
dotnet test --filter "FullyQualifiedName=Ui.KitchenApplitools.DragDropTests.DragAndDrop_StandardActions_OrderTicketContainsItem"
```

## Implementation Details

### UI Tests (Selenium WebDriver)

The UI tests verify the drag and drop functionality of Kitchen Applitools using:

- **Page Object Model**: The `KitchenPage` class encapsulates all interactions with the Kitchen Applitools website
- **WebDriver Factory**: The `WebDriverFactory` class manages WebDriver instances with thread safety
- **Parameterized Tests**: Tests support different menu items ("Fried Chicken", "Hamburger", "Ice Cream")
- **Multiple Drag-and-Drop Methods**:
  - Standard Actions API implementation
  - JavaScript Executor fallback for better compatibility
- **Visual Validation**: Screenshot comparison using Verify.ImageSharp.Compare
- **Comprehensive Logging**: All test steps are logged with Serilog
- **Test Utilities**: Helper methods for screenshot capture and comparison
- **Test Resources**: Verified baseline images for visual validation
- Both screenshots one that verified and one that captured are saved in the `tests/Ui.KitchenApplitools/bin/Debug/net9.0/Resources/Screenshots/` folder
- Tests could run using Chrome, Firefox, and Edge browsers

### API Tests (RestSharp)

The API tests verify the functionality of ReqRes API by:

- **API Client Interface**: `IReqResApiClient` defines the contract for API operations
- **Centralized Endpoints**: `TestEndpoints` class manages all API endpoint URLs
- **Modular Models**: Separate model classes for requests and responses
- **Comprehensive Logging**: All HTTP requests and responses are logged

## Visual Validation

The UI tests include visual validation of the drag-and-drop functionality:

1. Each test captures a screenshot after the drag-and-drop action
2. The screenshot is compared to a verified baseline image
3. The test passes if the images match within a defined threshold
