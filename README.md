# Instagram

Instagram is an Instagram Clone created as a test project for my ASP.NET MVC training at Devsinc. It doesn't have the same UI as the original Instagram as the motive behind this was mainly the functionality. For this project, I used .NET 4.8 and Entity Framework 6.0.

## Design Flow

The app is divided into 3 seperate layer.
1. Data Layer
2. Service Layer
3. Presentation Layer

#### Data Layer
The Data Layer includes all the models and the DB Context file. This layer is mainly responsible for setting up the database side of the application. It also includes some ViewModels which are not migrated to the Database as they are only needed locally to the application.

#### Service Layer
This layer is the core of the application. This is where all the functionlities lie. All the CRUD operations, all the data access queries and data validations are performed in this layer.

#### Presentation Layer
This layer is for what is presented to the User and to call functionality from the Service Layer to perform actions on the database. This layer includes:
1. Views
2. Controllers

The views are rendered to the user and the controller actions retrieve data and call functions from the service layer.

## Application Flow
When a GET request is made on the frontend then a controller action is called which returns the respective view.

When a POST reqest is made on the frontend then a controller action is called which passes data to the service layer. The service layer then processes the data and makes calls to the DBContext class in order to make updates to the Database

## Functionalities Added
1. User login/signup
2. Create Post
3. Edit Post
4. Delete Post
5. Create Story
6. Delete Story
7. Add Comments
8. Edit Comments
9. Delete Comments
10. Delete any comments on your post
11. Follow User
12. Change account privacy
13. Search for users
