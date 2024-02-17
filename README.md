![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/701491fc-1c46-466e-877d-27465a1d6574)# This is sample cards management using C# .NET 8, with Clean Architecture structure

## How to setup the project
1. Clone the project and run via Visual Studio 2022 for testing purpose. Make sure .NET 8.0 SDK is installed before proceeding.
2. It uses SQL Server database. Changes connection string in AppSettings.json file to point your local/remote database.

    ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/831016ae-3582-4ec7-bc37-c3573179bccd)

4. code first as been used. Please run database script via console shown below:
   
    ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/dddea23e-434c-4204-946b-a8357845f242)

5. Once the command executed, should expect database tables created i.e. Aspnetusers, etc

    ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/1d518ff7-9ace-4236-a047-6c168b8aef21)

 
6. When the application is executed for the first time after database migration, the data for users are seeded as show below

   a.commdand prompt
   
      ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/d8c2495f-f68c-4e5b-ba54-69a523741e66)


    b. Database confirmation
   
   
      ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/00840c63-438f-41bc-ab00-d5ebac3ddab8)
   
      ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/7d9750c2-cd11-420f-8199-f5cad221c4c5)
   
      ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/01282592-5c04-4c12-b6ca-9b81e11d8783)

   
7. Use below accounts for login
   
   a. Admin access  Email=admin@cardsmanagement.test, Password=admin@Admin123
   
   b. Member access  Email=member@cardsmanagement.test, Password=member@User123
  



    ## How to obtain token/login
     1. Use endpoint as show in the screenshot below then supply credentials above. You can interact directly or use any other tool that is capable to Restful API interaction. e.g. POSTMAN     https://www.postman.com/downloads/ 

      ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/1087e46f-a1f1-4844-8353-113fd95624c2)

    ### Successful Login
     ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/234e9192-dabe-450f-aa95-107038874870)

    ### Check content of the token using the portal https://jwt.io/

     ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/6f36a40a-3dce-44cb-9f79-688a4617a934)

    ### To interact the endpoint via swagger. Follow steps below
     
     ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/29abe724-86e0-44ef-b5b2-e717ebeebadc)


     ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/f87dc057-6f56-4f6c-ad30-136de2057252)

     ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/c3847a7a-8a0f-45a2-bef4-9d9b5c144dc9)



   ## Interacting with Cards Management Endpoint

    ![image](https://github.com/KIPROTICHK/CardsManagement/assets/11135927/a1c01b42-49f1-461f-8fcc-0bfafd502034)

   ### 1. Add Card by User

   

