# Intake

![Blazor](https://img.shields.io/badge/Blazor-512BD4?style=for-the-badge&logo=blazor&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![XML](https://img.shields.io/badge/XML-E34F26?style=for-the-badge&logo=w3c&logoColor=white)
![Time Taken](https://img.shields.io/badge/Time%20Taken-~1.5%20days-blue?style=for-the-badge&logo=clockify&logoColor=white)

Test 1 : A  simple Blazor application to capture,search, list, edit, and delete user information. User data (name, surname, cellphone number) is persisted to an XML file.

## Demo

![e4-intake-demo](https://github.com/user-attachments/assets/6f1c8d9d-2d66-4d95-831c-e83b9ca0108b)

**How it Works:**

1. **Search** — Type a keyword into the search bar and hit search. The user list filters to show matching results.
2. **Add** — Click the **Add** button to open a dialog. Fill in name, surname, and cellphone number. The form validates input before saving the new user to the XML file.
3. **Edit** — Click the ✏️ icon on any row to open the edit dialog with the user's current details pre-filled. Update the fields and click **Save** to persist changes.
4. **Delete** — Click the 🗑️ icon on any row to remove that user from the XML file.

## Assumptions & Business Rules

**ID Generation**: User IDs are generated using random numbers. I'm aware this introduces a risk of collision. In a production application, I would use GUIDs instead as they eliminate collision concerns and have the added benefit of being non-sequential, meaning users cannot predict or enumerate other users' IDs.
**Cellphone Number Uniqueness** : Cellphone numbers are treated as unique per user. This is based on the fact that South African mobile numbers are inherently unique to an individual. Name and surname are not subject to uniqueness checks as it's possible for multiple people to share the same name and surname. The application is scoped to South African numbers only, which is also why internationalization (e.g., country codes or international formatting) was not implemented.

## Decisions & Technical Considerations

**Repository & Service Split** : The data layer uses a repository/service separation. Because the persistence target is XML, there are a number of transformations and mappings involved in reading and writing data. Keeping that logic in a dedicated repository prevents the service layer from becoming cluttered and keeps each class focused on a single responsibility.

**Project Structure** : Given the small scope of the application, a single-project, folder-based structure is practical and keeps navigation simple. For a larger or growing codebase, I would move toward a clean architecture layout with separate projects (e.g., Domain, Application, Infrastructure, Presentation) to enforce clearer boundaries.

**Result Pattern** : A result pattern is used throughout the service layer so that every operation returns a structured response indicating success or failure. This makes it straightforward to give feedback to the user , in this case via a toast notification service.

**Toast Notifications (blazor-sonner)** : I opted for the blazor-sonner package rather than building a custom notification component. For an application of this size, it provides a clean UX with minimal setup time.

**Tailwind CSS over Bootstrap** : I chose Tailwind CSS instead of Bootstrap for finer-grained control over the styling. While utility classes are currently applied inline on elements, this could be refined by extracting reusable component styles using Tailwind's `@apply` directive or component abstractions. The key advantage is flexibility,  Tailwind doesn't impose opinionated design patterns, which makes it easier to achieve a custom look without fighting the framework.

**Testing Approach** : Given that this is a UI-heavy application, correctness was validated through manual testing. In a production scenario, I would add unit tests targeting the repository and service layers specifically.

**Possible Improvements**
- Add a confirmation dialog before destructive or irreversible actions (e.g., deleting a user, overwriting existing data) to reduce accidental data loss.
- Introduce unit tests for the repository and service layers to catch regressions early.
- Extract repeated Tailwind utility classes into reusable styles for better maintainability.

## Getting Started

```bash
git clone https://github.com/Intake.git
cd Intake
dotnet restore src/
dotnet run --project src/
```

The app will be available at on the port shown in your terminal).
