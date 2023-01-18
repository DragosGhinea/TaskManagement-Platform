using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProiectTaskManagement.Data;
using ProiectTaskManagement.Models.Entities;

namespace ProiectTaskManagement.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService
                <DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new IdentityRole { Id = "68500b9f-0485-4382-9c11-2898c3a6d108", Name = "Admin", NormalizedName = "Admin".ToUpper() },
                        //    new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7211", Name = "Editor", NormalizedName = "Editor".ToUpper() },
                        new IdentityRole { Id = "d3407dd8-3095-4e70-8db4-e8b127297326", Name = "User", NormalizedName = "User".ToUpper() }
                    );
                }

                var hasher = new PasswordHasher<Entities.AppUser>();


                context.Users.AddRange(
                    new Entities.AppUser
                    {
                        Id = "149d87af-a900-431d-af7b-1ccb1247001c", // primary key
                        UserName = "ADMIN",
                        FirstName = "Admin",
                        LastName = "Admin",
                        EmailConfirmed = true,
                        NormalizedEmail = "ADMIN@TEST.COM",
                        Email = "admin@test.com",
                        NormalizedUserName = "ADMIN",
                        PasswordHash = hasher.HashPassword(null, "Admin1!")
                    },



                    new Entities.AppUser
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb2", // primary key
                        UserName = "User2",
                        FirstName = "Ilie",
                        LastName = "Dumitrescu",
                        EmailConfirmed = true,
                        NormalizedEmail = "USER2@TEST.COM",
                        Email = "user2@test.com",
                        NormalizedUserName = "USER2",
                        PasswordHash = hasher.HashPassword(null, "User2!")
                    },



                    new Entities.AppUser
                    {
                        Id = "6a2fc684-80f8-424b-a79a-ba334b72924f", // primary key
                        UserName = "User3",
                        FirstName = "Bogdan",
                        LastName = "Manolescu",
                        EmailConfirmed = true,
                        NormalizedEmail = "USER3@TEST.COM",
                        Email = "user3@test.com",
                        NormalizedUserName = "USER3",
                        PasswordHash = hasher.HashPassword(null, "User3!")
                    },



                    new Entities.AppUser
                    {
                        Id = "b5abf238-41a5-4270-a439-373b4b8a60e6", // primary key
                        UserName = "User4",
                        FirstName = "Grigore",
                        LastName = "Moisil",
                        EmailConfirmed = true,
                        NormalizedEmail = "USER4@TEST.COM",
                        Email = "user4@test.com",
                        NormalizedUserName = "USER4",
                        PasswordHash = hasher.HashPassword(null, "User4!")
                    }
                ,


                    new Entities.AppUser
                    {
                        Id = "23b7e21c-a398-43b5-a12e-235e31538427", // primary key
                        UserName = "User5",
                        FirstName = "Marius",
                        LastName = "Manolescu",
                        EmailConfirmed = true,
                        NormalizedEmail = "USER5@TEST.COM",
                        Email = "user5@test.com",
                        NormalizedUserName = "USER5",
                        PasswordHash = hasher.HashPassword(null, "User5!")
                    }
                ,


                    new Entities.AppUser
                    {
                        Id = "8d23138f-e462-43a5-bd69-94208c3da94c", // primary key
                        UserName = "User6",
                        FirstName = "Dan",
                        LastName = "Nimara",
                        EmailConfirmed = true,
                        NormalizedEmail = "USER6@TEST.COM",
                        Email = "user6@test.com",
                        NormalizedUserName = "USER6",
                        PasswordHash = hasher.HashPassword(null, "User6!")
                    }
                ,


                    new Entities.AppUser
                    {
                        Id = "0c252b5c-734b-420d-bafe-0648f0d60282", // primary key
                        UserName = "User7",
                        FirstName = "Gigi",
                        LastName = "Parasachivescu",
                        EmailConfirmed = true,
                        NormalizedEmail = "USER7@TEST.COM",
                        Email = "user7@test.com",
                        NormalizedUserName = "USER7",
                        PasswordHash = hasher.HashPassword(null, "User7!")
                    }
                ,


                    new Entities.AppUser
                    {
                        Id = "d93be46c-b79d-456c-abf8-51d5ea3981e2", // primary key
                        UserName = "User8",
                        FirstName = "Cosmin",
                        LastName = "Bitu",
                        EmailConfirmed = true,
                        NormalizedEmail = "USER8@TEST.COM",
                        Email = "user8@test.com",
                        NormalizedUserName = "USER8",
                        PasswordHash = hasher.HashPassword(null, "User8!")
                    }
                ,


                    new Entities.AppUser
                    {
                        Id = "56edc504-b5a9-41b0-8d99-0047836c0982", // primary key
                        UserName = "User9",
                        FirstName = "Marinela",
                        LastName = "Paraschivescu",
                        EmailConfirmed = true,
                        NormalizedEmail = "USER9@TEST.COM",
                        Email = "user9@test.com",
                        NormalizedUserName = "USER9",
                        PasswordHash = hasher.HashPassword(null, "User9!")
                    }
                ,


                    new Entities.AppUser
                    {
                        Id = "c6061ff8-a802-4209-91f1-c9afe91a126b", // primary key
                        UserName = "User10",
                        FirstName = "Gabi",
                        LastName = "Romanescu",
                        EmailConfirmed = true,
                        NormalizedEmail = "USER10@TEST.COM",
                        Email = "user10@test.com",
                        NormalizedUserName = "USER10",
                        PasswordHash = hasher.HashPassword(null, "User10!")
                    }
                ,


                    new Entities.AppUser
                    {
                        Id = "ab17e226-8a03-459d-bdc8-90e68c72db9c", // primary key
                        UserName = "DragosG",
                        FirstName = "Dragos",
                        LastName = "Ghinea",
                        EmailConfirmed = true,
                        NormalizedEmail = "DRAGOSG@TEST.COM",
                        Email = "DragosG@test.com",
                        NormalizedUserName = "DRAGOSG",
                        PasswordHash = hasher.HashPassword(null, "Dragos123!")
                    }
                ,


                    new Entities.AppUser
                    {
                        Id = "1d522d03-e5cb-4da1-a19a-763ec646f500", // primary key
                        UserName = "BogdanI",
                        FirstName = "Bogdan",
                        LastName = "Iliescu",
                        EmailConfirmed = true,
                        NormalizedEmail = "BOGDANI@TEST.COM",
                        Email = "bogdani@test.com",
                        NormalizedUserName = "BOGDANI",
                        PasswordHash = hasher.HashPassword(null, "Bogdan123!")
                    }
                );



                context.Projects.AddRange(
                    new Entities.Project
                    {
                        Id = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        Title = "Vulnerability Management for CloudMining Com.",
                        CreationDate = new DateTime(DateTime.Now.Year - 1, 1, 20, 13, DateTime.Now.Minute, DateTime.Now.Second),
                        TasksContor = 5
                    }
                    ,


                    new Entities.Project
                    {
                        Id = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        Title = "SIEM Implementation Ames Comp.",
                        CreationDate = new DateTime(DateTime.Now.Year - 2, 12, 25, 14, DateTime.Now.Minute, DateTime.Now.Second),
                        TasksContor = 8
                    }
                    ,


                    new Entities.Project
                    {
                        Id = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        Title = "Cloud Sec. Assess for Cigna",
                        CreationDate = new DateTime(DateTime.Now.Year - 1, 9, 21, 11, DateTime.Now.Minute, DateTime.Now.Second),
                        TasksContor = 12
                    }
                    ,


                    new Entities.Project
                    {
                        Id = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        Title = "Pass Manage. Implem. for Kroge",
                        CreationDate = new DateTime(DateTime.Now.Year - 2, 5, 15, 15, DateTime.Now.Minute, DateTime.Now.Second),
                        TasksContor = 16
                    }
                    ,



                    new Entities.Project
                    {
                        Id = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        Title = "Compliance Audit Log of Dell",
                        CreationDate = new DateTime(DateTime.Now.Year - 1, 5, 1, 16, DateTime.Now.Minute, DateTime.Now.Second),
                        TasksContor = 19
                    }
                    ,


                    new Entities.Project
                    {
                        Id = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        Title = "Network Setup for FedEx",
                        CreationDate = new DateTime(DateTime.Now.Year - 1, 3, 4, 12, DateTime.Now.Minute, DateTime.Now.Second),
                        TasksContor = 24
                    }

                    );




                context.Statuses.AddRange(
                    new Entities.Status
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        Id = 1,
                        Color = "#8fce00",
                        Name = "Done"
                    }
                    ,


                    new Entities.Status
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        Id = 2,
                        Color = "#999999",
                        Name = "In Progress"
                    }
                    ,


                    new Entities.Status
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        Id = 3,
                        Color = "#cc0000",
                        Name = "After Deadline"
                    }


                /// project 2

                ,
                    new Entities.Status
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        Id = 3,
                        Color = "#cc0000",
                        Name = "After Deadline"
                    },




                    new Entities.Status
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        Id = 1,
                        Color = "#8fce00",
                        Name = "Done"
                    }
                    ,


                    new Entities.Status
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        Id = 2,
                        Color = "#999999",
                        Name = "In Progress"
                    }
                    ,


                    /// project 3
                    /// 


                    new Entities.Status
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        Id = 1,
                        Color = "#8fce00",
                        Name = "Done"
                    }
                    ,


                    new Entities.Status
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        Id = 2,
                        Color = "#999999",
                        Name = "In Progress"
                    }
                    ,



                    new Entities.Status
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        Id = 3,
                        Color = "#cc0000",
                        Name = "After Deadline"
                    }
                   ,

                    /// project 4
                    /// 

                    new Entities.Status
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        Id = 1,
                        Color = "#8fce00",
                        Name = "Done"
                    }
                    ,


                    new Entities.Status
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        Id = 2,
                        Color = "#999999",
                        Name = "In Progress"
                    }
                    ,



                    new Entities.Status
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        Id = 3,
                        Color = "#cc0000",
                        Name = "After Deadline"
                    }
                    ,

                    /// project 5
                    /// 

                    new Entities.Status
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        Id = 1,
                        Color = "#8fce00",
                        Name = "Done"
                    }
                    ,

                    new Entities.Status
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        Id = 2,
                        Color = "#999999",
                        Name = "In Progress"
                    }
                    ,

                    new Entities.Status
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        Id = 3,
                        Color = "#cc0000",
                        Name = "After Deadline"
                    }
                    ,

                    /// project 6
                    /// 


                    new Entities.Status
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        Id = 1,
                        Color = "#8fce00",
                        Name = "Client approved"
                    }
                    ,

                    new Entities.Status
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        Id = 2,
                        Color = "#999999",
                        Name = "Sent to Client"
                    }
                    ,

                    new Entities.Status
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        Id = 3,
                        Color = "#cc0000",
                        Name = "Client declined"
                    }

                    );





                /// taskuri

                context.Tasks.AddRange(
                    new Entities.Task
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        Id = 1,
                        Title = "Task 1",
                        Description = "Identify vulnerabilities in a client's systems and applications",
                        StatusId = 2,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 2, 12, 13, 4),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 23, 12, 13, 4)
                    }
                    ,


                    new Entities.Task
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        Id = 2,
                        Title = "Task 2",
                        Description = "Prioritize identified vulnerabilities based on risk level",
                        StatusId = 2,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 3, 10, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31, 11, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,


                    new Entities.Task
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        Id = 3,
                        Title = "Task 3",
                        Description = "Recommend and implement remediation measures to address high-priority vulnerabilities",
                        StatusId = 3,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 2, 11, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 21, 10, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,


                    new Entities.Task
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        Id = 4,
                        Title = "Task 4",
                        Description = "Monitor for the emergence of new vulnerabilities",
                        StatusId = 2,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 4, 11, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, 2, 23, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,


                    new Entities.Task
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        Id = 5,
                        Title = "Task 5",
                        Description = "Provide regular reports on the status of vulnerabilities and remediation efforts",
                        StatusId = 1,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 4, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 17, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,

                    /// project 2
                    /// 


                    new Entities.Task
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        Id = 6,
                        Title = "Task 1",
                        Description = "Design a custom SIEM solution to meet the client's needs",
                        StatusId = 1,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 2, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 18, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,


                    new Entities.Task
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        Id = 7,
                        Title = "Task 2",
                        Description = "Assist with the implementation of the SIEM",
                        StatusId = 2,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 11, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 21, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,


                    new Entities.Task
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        Id = 8,
                        Title = "Task 3",
                        Description = "Train client personnel on how to use and manage the SIEM",
                        StatusId = 1,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 5, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 18, 1, DateTime.Now.Minute, DateTime.Now.Second)
                    },


                    /// project 3
                    /// 


                    new Entities.Task
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        Id = 9,
                        Title = "Task 1",
                        Description = "Recommend and implement security controls to address identified risks",
                        StatusId = 2,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 3, 11, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 23, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,


                    new Entities.Task
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        Id = 10,
                        Title = "Task 2",
                        Description = "Test the effectiveness of the implemented security controls",
                        StatusId = 2,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 6, 11, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, 2, 22, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                   ,




                    new Entities.Task
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        Id = 11,
                        Title = "Task 3",
                        Description = "Provide a report detailing the findings and recommendations",
                        StatusId = 2,
                        StartDate = new DateTime(DateTime.Now.Year, 1, 5, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, 12, 23, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,



                    new Entities.Task
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        Id = 12,
                        Title = "Task 4",
                        Description = "Assist with the implementation of any additional recommended measures",
                        StatusId = 2,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7, 11, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year + 1, 1, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,



                    /// project 4
                    /// 



                    new Entities.Task
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        Id = 13,
                        Title = "Task 1",
                        Description = "Review client's current password management practices and identify areas for improvement",
                        StatusId = 2,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7, 11, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year + 1, 1, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,



                    new Entities.Task
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        Id = 14,
                        Title = "Task 2",
                        Description = "Design a custom password management solution to meet the client's needs",
                        StatusId = 2,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 6, 11, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year + 1, 1, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,

                    new Entities.Task
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        Id = 15,
                        Title = "Task 3",
                        Description = "Assist with the implementation of the password management solution",
                        StatusId = 1,
                        StartDate = new DateTime(2020, DateTime.Now.Month, 15, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(2021, DateTime.Now.Month, 18, 1, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,
                    new Entities.Task
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        Id = 16,
                        Title = "Task 4",
                        Description = "Train client personnel on how to use and manage the password management system",
                        StatusId = 3,
                        StartDate = new DateTime(DateTime.Now.Year, 1, 1, 9, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, 1, 06, 10, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,
                    new Entities.Task
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        Id = 17,
                        Title = "Task 1",
                        Description = "Review client's systems and processes to ensure compliance with relevant industry regulations and standards",
                        StatusId = 1,
                        StartDate = new DateTime(2020, 1, 1, 9, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(2021, 1, 06, 10, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,
                    new Entities.Task
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        Id = 18,
                        Title = "Task 2",
                        Description = "Identify any areas of non-compliance",
                        StatusId = 2,
                        StartDate = new DateTime(2021, 1, 09, 10, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, 1, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,
                    new Entities.Task
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        Id = 19,
                        Title = "Task 3",
                        Description = "Recommend and assist with the implementation of remediation measures to address non-compliance",
                        StatusId = 1,
                        StartDate = new DateTime(2021, 1, 1, 9, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(2022, 1, 06, 10, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,






                    /// project 6
                    /// 



                    new Entities.Task
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        Id = 20,
                        Title = "Task 1",
                        Description = "Review client's network architecture and identify areas where segmentation could improve security",
                        StatusId = 3,
                        StartDate = new DateTime(2020, 5, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(2021, 1, 23, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                   ,
                    new Entities.Task
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        Id = 21,
                        Title = "Task 2",
                        Description = "Design a plan for implementing network segmentation in the client's environment",
                        StatusId = 1,
                        StartDate = new DateTime(2020, 1, 1, 9, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(2022, 1, 06, 10, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,
                    new Entities.Task
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        Id = 22,
                        Title = "Task 3",
                        Description = "Assist with the implementation of the network segmentation plan",
                        StatusId = 2,
                        StartDate = new DateTime(DateTime.Now.Year - 1, 12, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, 1, 5, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                   ,
                    new Entities.Task
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        Id = 23,
                        Title = "Task 4",
                        Description = "Test the effectiveness of the implemented segmentation",
                        StatusId = 3,
                        StartDate = new DateTime(DateTime.Now.Year - 1, 12, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, 1, 23, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,
                    new Entities.Task
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        Id = 24,
                        Title = "Task 5",
                        Description = "Provide ongoing support and maintenance for the segmentation solution.",
                        StatusId = 1,
                        StartDate = new DateTime(2020, 12, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(2022, 5, 6, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    );








                /// teammembers






                context.TeamMembers.AddRange(
                    new Entities.TeamMember
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        AppUserId = "c6061ff8-a802-4209-91f1-c9afe91a126b",
                        AddedByUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 1, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                   ,
                    new Entities.TeamMember
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        AppUserId = "1d522d03-e5cb-4da1-a19a-763ec646f500",
                        AddedByUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 2, 23, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AddedByUserId = null,
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 1, 20, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    },


                    /// project 2 team members
                    /// 


                    new Entities.TeamMember
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        AppUserId = "c6061ff8-a802-4209-91f1-c9afe91a126b",
                        AddedByUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 4, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        AppUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        AddedByUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 3, 22, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AddedByUserId = null,
                        JoinDate = new DateTime(DateTime.Now.Year - 2, 12, 25, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        AppUserId = "23b7e21c-a398-43b5-a12e-235e31538427",
                        AddedByUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 4, 27, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,


                    /// project 3 team members
                    /// 


                    new Entities.TeamMember
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        AppUserId = "23b7e21c-a398-43b5-a12e-235e31538427",
                        AddedByUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 10, 21, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        AppUserId = "0c252b5c-734b-420d-bafe-0648f0d60282",
                        AddedByUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 10, 25, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AddedByUserId = null,
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 9, 21, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,


                    /// project 4



                    new Entities.TeamMember
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AddedByUserId = "56edc504-b5a9-41b0-8d99-0047836c0982",
                        JoinDate = new DateTime(DateTime.Now.Year - 2, 6, 21, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        AppUserId = "ab17e226-8a03-459d-bdc8-90e68c72db9c",
                        AddedByUserId = "56edc504-b5a9-41b0-8d99-0047836c0982",
                        JoinDate = new DateTime(DateTime.Now.Year - 2, 6, 23, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        AppUserId = "56edc504-b5a9-41b0-8d99-0047836c0982",
                        AddedByUserId = null,
                        JoinDate = new DateTime(DateTime.Now.Year - 2, 5, 15, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        AppUserId = "0c252b5c-734b-420d-bafe-0648f0d60282",
                        AddedByUserId = "56edc504-b5a9-41b0-8d99-0047836c0982",
                        JoinDate = new DateTime(DateTime.Now.Year - 2, 6, 19, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,


                    /// project 5
                    /// 



                    new Entities.TeamMember
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        AppUserId = "b5abf238-41a5-4270-a439-373b4b8a60e6",
                        AddedByUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 5, 19, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        AppUserId = "6a2fc684-80f8-424b-a79a-ba334b72924f",
                        AddedByUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 5, 20, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        AppUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        AddedByUserId = null,
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 5, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AddedByUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 5, 27, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,



                    /// project 6
                    /// 




                    new Entities.TeamMember
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AddedByUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 3, 23, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        AppUserId = "6a2fc684-80f8-424b-a79a-ba334b72924f",
                        AddedByUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 3, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        AppUserId = "1d522d03-e5cb-4da1-a19a-763ec646f500",
                        AddedByUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 3, 28, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        AppUserId = "ab17e226-8a03-459d-bdc8-90e68c72db9c",
                        AddedByUserId = null,
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 3, 20, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    ,
                    new Entities.TeamMember
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        AppUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        AddedByUserId = null,
                        JoinDate = new DateTime(DateTime.Now.Year - 1, 3, 4, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)

                    }
                    );




                //// comments



                context.Comments.AddRange(
                    new Entities.Comment
                    {
                        CommentId = "3c0f88f8-89aa-4b23-8d0f-d87912bb92fe",
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 1,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "Found a new SQL Vulnerability!",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 3, 11, 23, 6),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "2c2ac7c6-99bd-4819-b3cc-0264696c1841",
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 2,
                        AppUserId = "1d522d03-e5cb-4da1-a19a-763ec646f500",
                        Content = "I'm focused on SQL injection vuln! ",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 4, 11, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = null
                    }
                   ,
                    new Entities.Comment
                    {
                        CommentId = "89c4e1fb-39b0-4e60-a697-3c3e7ba1345d",
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 1,
                        AppUserId = "c6061ff8-a802-4209-91f1-c9afe91a126b",
                        Content = "Found a new Remote Execution Vulnerability!",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 6, 10, 23, 6),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "491b7d30-dc06-4a24-93ee-8670847a7b9a",
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 3,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "Talked to client to review the connection strings with the database!",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 5, 10, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "214d1b57-3e7f-4ac5-83d2-e94afa2d89d6",
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 4,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "Implemented some new rules for better recognize of sql vulnerabilities",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7, 10, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "ae4526f1-6617-461c-b0a2-68ef42cf0c55",
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 5,
                        AppUserId = "1d522d03-e5cb-4da1-a19a-763ec646f500",
                        Content = "Provided support Update: 7.01.2023",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7, 10, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,





                    /// comments with parents
                    /// 



                    new Entities.Comment
                    {
                        CommentId = "7ff820fd-b4a3-4d24-b858-7956468aa912",
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 1,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "Found a fix!",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 3, 15, 21, 6),
                        ParentId = "3c0f88f8-89aa-4b23-8d0f-d87912bb92fe"
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "75ca7000-68af-4d1a-9dcb-2a4f1ae81fe0",
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 3,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "The client aggred the fix!",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 6, 10, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = "491b7d30-dc06-4a24-93ee-8670847a7b9a"
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "bcd39b81-2f83-4711-b094-05c350fa8e33",
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 1,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "Found a fix!",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 6, 13, 12, 6),
                        ParentId = "89c4e1fb-39b0-4e60-a697-3c3e7ba1345d"
                    }
                   ,
                    new Entities.Comment
                    {
                        CommentId = "9f9f83c6-45f2-4b81-86d2-a4ae71067c2a",
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 4,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "The client agreed the new rules!",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 8, 10, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = "214d1b57-3e7f-4ac5-83d2-e94afa2d89d6"
                    }
                    ,







                    /// comments project 2
                    /// 


                    new Entities.Comment
                    {
                        CommentId = "bdd6d3e0-82a5-4b6a-aa03-ebbfe5e52edf",
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 6,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "Update from the client: they want to use the Splunk framework",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 2, 10, 23, 6),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "da37ca07-a839-44f2-ba9a-7c319854d1e9",
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 7,
                        AppUserId = "c6061ff8-a802-4209-91f1-c9afe91a126b",
                        Content = "Done IP Routing to user's endpoints",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 3, 14, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "6d786534-505e-4b9a-9cd7-1f08757172bf",
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 8,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "50% personeel trained",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7, 15, 11, 9),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "5b0caa6a-2852-48a0-8d16-33996329a2d2",
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 7,
                        AppUserId = "23b7e21c-a398-43b5-a12e-235e31538427",
                        Content = "Done Custom Firmware",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 4, 12, 34, 6),
                        ParentId = null
                    }
                    ,

                    /// comments with parents


                    new Entities.Comment
                    {
                        CommentId = "465aacc6-ca55-4589-9463-7d1850ecdc26",
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 6,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "After talking, they want some custom Splunk with personalized plugins",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 3, 11, 19, 6),
                        ParentId = "bdd6d3e0-82a5-4b6a-aa03-ebbfe5e52edf"
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "aff187ab-4dab-43eb-ae8c-0f9dccfd4f17",
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 7,
                        AppUserId = null,   /// user-ul a fost sters
                        Content = "Firmware V2 update: 6.01.2023",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 6, 12, 43, 20),
                        ParentId = "5b0caa6a-2852-48a0-8d16-33996329a2d2"
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "7a4684d6-be31-4997-b756-f570ddf872e8",
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 8,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "70% personeel trained",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 8, 15, 11, 9),
                        ParentId = "6d786534-505e-4b9a-9cd7-1f08757172bf"
                    }
                    ,


                    /// comments project 3
                    /// 



                    new Entities.Comment
                    {
                        CommentId = "45a4b28e-c28f-4d8c-b5f9-93a6a11e75a7",
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 9,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "Identified wrong connection string to the database!",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 4, 10, 13, 9),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "476326f8-c166-4c32-bce8-d79ff9f2d726",
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 9,
                        AppUserId = "0c252b5c-734b-420d-bafe-0648f0d60282",
                        Content = "Found a new leaked Google Drive link",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 4, 15, 13, 9),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "e4537605-1f00-4d82-bc92-5a3bcd608643",
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 10,
                        AppUserId = "0c252b5c-734b-420d-bafe-0648f0d60282",
                        Content = "Testing for new leaked client's cloud links",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7, 15, 13, 9),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "971e292b-1a95-4362-926b-9bc3cf27ebe8",
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 11,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "Provided to user a detailed report of all findings",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 8, 16, 11, 9),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "f38c851a-e5c1-4add-89f5-122c61339b74",
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 12,
                        AppUserId = "0c252b5c-734b-420d-bafe-0648f0d60282",
                        Content = "Talked with the Security Team and assured that all fixies are implemented",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 8, 22, 23, 9),
                        ParentId = null
                    }
                    ,



                    /// comments with parents
                    /// 



                    new Entities.Comment
                    {
                        CommentId = "7c63695f-751a-4316-8ef2-30ac0ad40c83",
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 10,
                        AppUserId = "0c252b5c-734b-420d-bafe-0648f0d60282",
                        Content = "Nothing found",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7, 20, 13, 9),
                        ParentId = "476326f8-c166-4c32-bce8-d79ff9f2d726"
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "82bb28d6-5ba5-4e68-9f7e-297a84b9f506",
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 9,
                        AppUserId = "0c252b5c-734b-420d-bafe-0648f0d60282",
                        Content = "Found a bypass!",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 4, 16, 26, 9),
                        ParentId = "476326f8-c166-4c32-bce8-d79ff9f2d726"
                    }
                   ,
                    new Entities.Comment
                    {
                        CommentId = "6febf7f9-850d-42cc-9622-f1845d9306dd",
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 11,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "Client aggred the report",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 8, 21, 11, 9),
                        ParentId = "971e292b-1a95-4362-926b-9bc3cf27ebe8"
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "63303c82-2f54-451b-8afe-af9a075e5de9",
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 12,
                        AppUserId = "0c252b5c-734b-420d-bafe-0648f0d60282",
                        Content = "Measures  V1 implemented!",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 8, 23, 11, 9),
                        ParentId = "f38c851a-e5c1-4add-89f5-122c61339b74"
                    }
                    ,




                    /// comments project 4
                    /// 


                    new Entities.Comment
                    {
                        CommentId = "7cc33809-b688-4613-95d9-7c68302cf975",
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 13,
                        AppUserId = "ab17e226-8a03-459d-bdc8-90e68c72db9c",
                        Content = "Found a misconfigured rule in current Password Manager",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 8, 8, 16, 6),
                        ParentId = null
                    }
                   ,
                    new Entities.Comment
                    {
                        CommentId = "46b04e2a-b672-49fa-b3ee-43bded6388b1",
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 14,
                        AppUserId = "ab17e226-8a03-459d-bdc8-90e68c72db9c",
                        Content = "Update: The client wants to use the Symantec framework",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7, 9, 35, 6),
                        ParentId = null
                    }
                   ,
                    new Entities.Comment
                    {
                        CommentId = "66213404-d897-4a8f-a204-8f8842f55415",
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 15,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "Succesfuly migrated all passwords to the new Password Manager",
                        CreationDate = new DateTime(2020, 5, 15, 10, 12, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "fa4780fe-dcb0-4099-a14c-7dfcee23cf7e",
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 16,
                        AppUserId = null,   /// angajatul a fost data afara
                        Content = "Started training personnel",
                        CreationDate = new DateTime(DateTime.Now.Year, 1, 4, 9, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,


                    //// comments with parents
                    ///



                    new Entities.Comment
                    {
                        CommentId = "846f0e48-cadc-4596-8305-15254e3091f2",
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 13,
                        AppUserId = "ab17e226-8a03-459d-bdc8-90e68c72db9c",
                        Content = "Fixed it",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 8, 10, 21, 6),
                        ParentId = "7cc33809-b688-4613-95d9-7c68302cf975"
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "53d060b6-1981-4c4b-a5bb-9232fbf56221",
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 14,
                        AppUserId = "56edc504-b5a9-41b0-8d99-0047836c0982",
                        Content = "Succesfully implemented",
                        CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7, 22, 35, 6),
                        ParentId = "46b04e2a-b672-49fa-b3ee-43bded6388b1"
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "0494dad0-e69a-4d28-8f6b-f693a5320fa8",
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 16,
                        AppUserId = "56edc504-b5a9-41b0-8d99-0047836c0982",
                        Content = "Unfortunately, the deadline has been overtaken. The task continues, but with another employee.",
                        CreationDate = new DateTime(DateTime.Now.Year, 1, 7, 9, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = "fa4780fe-dcb0-4099-a14c-7dfcee23cf7e"
                    }
                    ,




                    /// comments project 5
                    /// 





                    new Entities.Comment
                    {
                        CommentId = "b9033290-a507-4542-8447-9e9c3b6aebfb",
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        TaskId = 17,
                        AppUserId = "6a2fc684-80f8-424b-a79a-ba334b72924f",
                        Content = "Found a misconfigured database with actual system requirements",
                        CreationDate = new DateTime(2020, 1, 6, 9, 23, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "602949d3-40f2-4ff0-a97b-cee9326f442e",
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        TaskId = 18,
                        AppUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        Content = "Didn't found any non-compliance area!",
                        CreationDate = new DateTime(2021, 4, 15, 16, 26, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "582d8df4-a39a-4db3-b8dc-751384cc24d0",
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        TaskId = 19,
                        AppUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        Content = "Talked with the Security Team and assured that all fixies are implemented",
                        CreationDate = new DateTime(2022, 1, 05, 10, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,



                    //// comments with parents
                    ///




                    new Entities.Comment
                    {
                        CommentId = "82453462-4618-4e58-a0f1-90301395c2ad",
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        TaskId = 17,
                        AppUserId = "6a2fc684-80f8-424b-a79a-ba334b72924f",
                        Content = "Found a bypass!",
                        CreationDate = new DateTime(2020, 1, 7, 11, 23, DateTime.Now.Second),
                        ParentId = "b9033290-a507-4542-8447-9e9c3b6aebfb"
                    }
                    ,





                    /// comments project 6
                    /// 




                    new Entities.Comment
                    {
                        CommentId = "366b802f-9a0c-42fa-9f74-5ed367cb022b",
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 20,
                        AppUserId = "1d522d03-e5cb-4da1-a19a-763ec646f500",
                        Content = "Found an area that can be improved, Delivery routes",
                        CreationDate = new DateTime(2020, 6, 14, 12, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "01a71540-3d7a-4ecc-9c9c-a06e5296a162",
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 21,
                        AppUserId = "6a2fc684-80f8-424b-a79a-ba334b72924f",
                        Content = "Developing a new network diagram for client",
                        CreationDate = new DateTime(2021, 4, 7, 18, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "e73982c9-6bc4-4c8f-abdf-ca43c0d99064",
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 22,
                        AppUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        Content = "Trained the personnel. Waiting for the approvement",
                        CreationDate = new DateTime(DateTime.Now.Year - 1, 12, 28, 15, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "093137d2-2b4c-4367-b177-a7c172434ef7",
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 23,
                        AppUserId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        Content = "Started the tests for effectivenes of our systems",
                        CreationDate = new DateTime(DateTime.Now.Year - 1, 12, 29, 13, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = null
                    }
                    ,



                    //// comments with parents
                    ///




                    new Entities.Comment
                    {
                        CommentId = "195a2aa3-a937-4aea-a5bc-dd62110a4d23",
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 20,
                        AppUserId = "1d522d03-e5cb-4da1-a19a-763ec646f500",
                        Content = "Fix implemented, but the cliend refused it",
                        CreationDate = new DateTime(2021, 1, 14, 12, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = "366b802f-9a0c-42fa-9f74-5ed367cb022b"
                    }
                    ,
                    new Entities.Comment
                    {
                        CommentId = "6323cc67-ac3f-4913-b398-0ed3c6264963",
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 22,
                        AppUserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        Content = "Still no answer by the client, update: 8.01.2023",
                        CreationDate = new DateTime(2023, 1, 8, 13, DateTime.Now.Minute, DateTime.Now.Second),
                        ParentId = "e73982c9-6bc4-4c8f-abdf-ca43c0d99064"
                    }
                    );







                /// task asssigns project 1
                /// 





                context.TaskAssigns.AddRange(
                    new Relationships.TaskAssign
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 1,
                        TeamMemberId = "c6061ff8-a802-4209-91f1-c9afe91a126b",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 1,
                        TeamMemberId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 2,
                        TeamMemberId = "1d522d03-e5cb-4da1-a19a-763ec646f500",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                   ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 3,
                        TeamMemberId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 4,
                        TeamMemberId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                   ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "cde48f4d-a1b4-4afd-be19-f047caa5a455",
                        TaskId = 5,
                        TeamMemberId = "1d522d03-e5cb-4da1-a19a-763ec646f500",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                    ,


                    /// task assign project 2
                    /// 


                    new Relationships.TaskAssign
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 6,
                        TeamMemberId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                    ,


                    new Relationships.TaskAssign
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 6,
                        TeamMemberId = "c6061ff8-a802-4209-91f1-c9afe91a126b",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                   ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 7,
                        TeamMemberId = "c6061ff8-a802-4209-91f1-c9afe91a126b",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 7,
                        TeamMemberId = "23b7e21c-a398-43b5-a12e-235e31538427",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "11ce4ab2-dad1-466a-8c94-870ae048a985",
                        TaskId = 8,
                        TeamMemberId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                    ,

                    /// atribuiti project 3




                    new Relationships.TaskAssign
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 9,
                        TeamMemberId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                   ,
                   new Relationships.TaskAssign
                   {
                       ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                       TaskId = 9,
                       TeamMemberId = "0c252b5c-734b-420d-bafe-0648f0d60282",
                       AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                   }
                   ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 10,
                        TeamMemberId = "0c252b5c-734b-420d-bafe-0648f0d60282",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 11,
                        TeamMemberId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "60d128bf-f464-48e2-9f7a-9052cb6d1146",
                        TaskId = 12,
                        TeamMemberId = "0c252b5c-734b-420d-bafe-0648f0d60282",
                        AssignedById = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                   ,



                    /// task assign project 4
                    /// 



                    new Relationships.TaskAssign
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 13,
                        TeamMemberId = "ab17e226-8a03-459d-bdc8-90e68c72db9c",
                        AssignedById = "56edc504-b5a9-41b0-8d99-0047836c0982"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 14,
                        TeamMemberId = "ab17e226-8a03-459d-bdc8-90e68c72db9c",
                        AssignedById = "56edc504-b5a9-41b0-8d99-0047836c0982"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 14,
                        TeamMemberId = "56edc504-b5a9-41b0-8d99-0047836c0982",
                        AssignedById = "56edc504-b5a9-41b0-8d99-0047836c0982"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 15,
                        TeamMemberId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AssignedById = "56edc504-b5a9-41b0-8d99-0047836c0982"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a39f1929-3576-4e3b-8140-d8e533a0973b",
                        TaskId = 16,
                        TeamMemberId = "56edc504-b5a9-41b0-8d99-0047836c0982",
                        AssignedById = "56edc504-b5a9-41b0-8d99-0047836c0982"
                    }
                    ,


                    /// task assign project 5
                    /// 



                    new Relationships.TaskAssign
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        TaskId = 17,
                        TeamMemberId = "6a2fc684-80f8-424b-a79a-ba334b72924f",
                        AssignedById = "d93be46c-b79d-456c-abf8-51d5ea3981e2"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        TaskId = 18,
                        TeamMemberId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        AssignedById = "d93be46c-b79d-456c-abf8-51d5ea3981e2"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        TaskId = 19,
                        TeamMemberId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        AssignedById = "d93be46c-b79d-456c-abf8-51d5ea3981e2"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        TaskId = 17,
                        TeamMemberId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AssignedById = "d93be46c-b79d-456c-abf8-51d5ea3981e2"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "4513b0b5-7b69-4ed0-8217-f4ce0ce2dd4b",
                        TaskId = 18,
                        TeamMemberId = "b5abf238-41a5-4270-a439-373b4b8a60e6",
                        AssignedById = "d93be46c-b79d-456c-abf8-51d5ea3981e2"
                    }
                    ,





                    /// task assign project 6
                    /// 




                    new Relationships.TaskAssign
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 20,
                        TeamMemberId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AssignedById = "d93be46c-b79d-456c-abf8-51d5ea3981e2"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 20,
                        TeamMemberId = "1d522d03-e5cb-4da1-a19a-763ec646f500",
                        AssignedById = "d93be46c-b79d-456c-abf8-51d5ea3981e2"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 21,
                        TeamMemberId = "6a2fc684-80f8-424b-a79a-ba334b72924f",
                        AssignedById = "d93be46c-b79d-456c-abf8-51d5ea3981e2"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 22,
                        TeamMemberId = "1d522d03-e5cb-4da1-a19a-763ec646f500",
                        AssignedById = "ab17e226-8a03-459d-bdc8-90e68c72db9c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 22,
                        TeamMemberId = "ab17e226-8a03-459d-bdc8-90e68c72db9c",
                        AssignedById = "ab17e226-8a03-459d-bdc8-90e68c72db9c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 22,
                        TeamMemberId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        AssignedById = "ab17e226-8a03-459d-bdc8-90e68c72db9c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 23,
                        TeamMemberId = "d93be46c-b79d-456c-abf8-51d5ea3981e2",
                        AssignedById = "ab17e226-8a03-459d-bdc8-90e68c72db9c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 23,
                        TeamMemberId = "6a2fc684-80f8-424b-a79a-ba334b72924f",
                        AssignedById = "ab17e226-8a03-459d-bdc8-90e68c72db9c"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 24,
                        TeamMemberId = "1d522d03-e5cb-4da1-a19a-763ec646f500",
                        AssignedById = "d93be46c-b79d-456c-abf8-51d5ea3981e2"
                    }
                    ,
                    new Relationships.TaskAssign
                    {
                        ProjectId = "a5075d35-4872-4524-b19d-f9f3b188ea27",
                        TaskId = 24,
                        TeamMemberId = "8d23138f-e462-43a5-bd69-94208c3da94c",
                        AssignedById = "d93be46c-b79d-456c-abf8-51d5ea3981e2"
                    }
                    );





                // USER-ROLE

                context.UserRoles.AddRange(
                    new IdentityUserRole<string>
                    {
                        RoleId = "68500b9f-0485-4382-9c11-2898c3a6d108",
                        UserId = "149d87af-a900-431d-af7b-1ccb1247001c"
                    }
                ,
                    new IdentityUserRole<string>
                    {
                        RoleId = "68500b9f-0485-4382-9c11-2898c3a6d108",
                        UserId = "ab17e226-8a03-459d-bdc8-90e68c72db9c"
                    }
               ,
                    new IdentityUserRole<string>
                    {
                        RoleId = "68500b9f-0485-4382-9c11-2898c3a6d108",
                        UserId = "1d522d03-e5cb-4da1-a19a-763ec646f500"
                    }
                ,
                    new IdentityUserRole<string>
                    {
                        RoleId = "d3407dd8-3095-4e70-8db4-e8b127297326",
                        UserId = "8e445865-a24d-4543-a6c6-9443d048cdb2"
                    }
                ,
                    new IdentityUserRole<string>
                    {
                        RoleId = "d3407dd8-3095-4e70-8db4-e8b127297326",
                        UserId = "6a2fc684-80f8-424b-a79a-ba334b72924f"
                    }
                ,
                    new IdentityUserRole<string>
                    {
                        RoleId = "d3407dd8-3095-4e70-8db4-e8b127297326",
                        UserId = "b5abf238-41a5-4270-a439-373b4b8a60e6"
                    }
                ,
                    new IdentityUserRole<string>
                    {
                        RoleId = "d3407dd8-3095-4e70-8db4-e8b127297326",
                        UserId = "23b7e21c-a398-43b5-a12e-235e31538427"
                    }
                ,
                    new IdentityUserRole<string>
                    {
                        RoleId = "d3407dd8-3095-4e70-8db4-e8b127297326",
                        UserId = "8d23138f-e462-43a5-bd69-94208c3da94c"
                    }
                ,
                    new IdentityUserRole<string>
                    {
                        RoleId = "d3407dd8-3095-4e70-8db4-e8b127297326",
                        UserId = "0c252b5c-734b-420d-bafe-0648f0d60282"
                    }
                ,
                    new IdentityUserRole<string>
                    {
                        RoleId = "d3407dd8-3095-4e70-8db4-e8b127297326",
                        UserId = "d93be46c-b79d-456c-abf8-51d5ea3981e2"
                    }
               ,
                    new IdentityUserRole<string>
                    {
                        RoleId = "d3407dd8-3095-4e70-8db4-e8b127297326",
                        UserId = "56edc504-b5a9-41b0-8d99-0047836c0982"
                    }
                ,
                    new IdentityUserRole<string>
                    {
                        RoleId = "d3407dd8-3095-4e70-8db4-e8b127297326",
                        UserId = "c6061ff8-a802-4209-91f1-c9afe91a126b"
                    }
                );

                try
                {
                    context.SaveChanges();
                }
                catch
                {

                }

            }
        }
    }
}