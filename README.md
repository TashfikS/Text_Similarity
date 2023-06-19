# Text Similarity Service

The Text Similarity Service is a RESTful API that analyzes the similarity between two text inputs. It calculates the percentage of unique words that are common between the texts, ignoring case and order. The service is implemented using ASP.NET Core 6.

## API Endpoints

### Text Similarity

- **Endpoint:** `POST /similarities`
- **Description:** Calculates the similarity between two texts.
- **Request Body:**
  - `text1` (string): The first text input.
  - `text2` (string): The second text input.
- **Response:**
  - `similarity` (number): The similarity percentage between the texts.
- **Example:**
  - Request Body:
    ```json
    {
      "text1": "The quick brown fox jumps over the lazy dog",
      "text2": "The dog was not amused"
    }
    ```
  - Response Body:
    ```json
    {
      "similarity": 36.67
    }
    ```

## Additional Requirements

The Text Similarity Service meets the following additional requirements:

- **Data Validation:** The service ensures that the input data is a valid string. If either `text1` or `text2` is missing or empty, a `400 Bad Request` response is returned.
- **Error Handling:** The service handles errors gracefully and returns appropriate HTTP status codes and error messages.
- **Testing:** Unit tests have been written to ensure the service functions as expected. The tests cover scenarios such as valid input and missing input, verifying the calculated similarity and error responses.
- **Documentation:** This README file serves as documentation, providing clear instructions on how to set up, run, and use the Text Similarity Service.

## Setup and Run

To set up and run the Text Similarity Service, follow these steps:

1. Clone the repository or download the source code.

2. Open the project in your preferred IDE (e.g., Visual Studio).

3. Build the solution to restore NuGet packages.

4. Run the project.

5. The service will start running and listening for requests on the specified port.

## Testing

The service can be tested using tools like Postman or by running the provided unit tests.

To run the unit tests:

1. Open the test project in your preferred IDE.

2. Build the solution to restore NuGet packages (if not already done).

3. Run the unit tests.

4. The tests will be executed, and the results will be displayed.