
# TaskManagement Application

A project made with **ASP.NET MVC 6** and EntityFramework that represents a task management platform.
You can register/login and create projects with multiple leaders, tasks that have various people assigned to them,
comments and statuses. More details below!

![MainPage](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/MainPagePreview.png)

## Main Features
The application contains:
- **Admin/User Roles** | Roles with different effects in the application.
     - Users have a projects page
     - Users can create projects
     - Users can be leaders/members
     - Admins have leader permission on any project
     - Admins can delete users (just not themselves)
- **Profile Pictures** | Default Profile Pictures + Customizable Uploaded Ones
- **Google Login** | Google Login Implemented
- **Modified Framework** | Modified the framework to allow different usernames than email. Added extra fields for user.
- **Admin Views** | All Projects & All Users List for Admins Only
     - **Pagination** | In these views there are 3 elements displayed per page
     - **Search** | In these views you can search users (by last name, first name, username or email) and projects (by title or in depth by user details)
- **CRUD On Most Objects**
     - Some objects have their  own views
     - Some objects have these operations accessible via modals in other objects' views

## Database Design
Altough the database is generated code first, based on models, it  was firstly designed as a diagram.

## Preview Images

![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview1.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview2.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview3.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview4.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview5.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview6.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview7.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview8.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview9.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview10.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview11.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview12.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview13.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview14.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview15.png)
![AppPreview](https://github.com/DragosGhinea/TaskManagement-Platform/Previews/blob/main/AppPreview16.png)

## Other Info
- This project was initially created in romanian and translated afterwards. The project might not have it's variables fully translated! (Sorry for that)
- This project was made for a university assignment
- The application is semi-responsive, was not properly tested nor was it intended to be fully responsive in the first place. It was tested on 2560 x 1600 resolution.
- This project was made in two weeks

