# scheduleApp

A basic Web app to schedule your day . It is made using React as frontend and Asp.net core as Backend server . It uses SQL server database as backend storage


# Setup

### Frontend
___

1. Obtain your Google OAuth Credentials

2. Place your Client secret key in `ExtraUtils.js` , which is present in `Frontend\scheduele-client\src\Utils`

```Javascript

  class ExtraUtils{
    constructor(){
        this.PortNumber = "YOUR PORT NUMBER HERE";
        this.ClientSecret = " YOUR CLIENT SECRET HERE ";
    }
}

```

3. Type the port number in ExtraUtils.js file , where the Backend server is hosted

4. Go to `Frontend\scheduele-client` and type `npm start` in your terminal

***

### Backend
___

1. Obtain your Google OAuth Credentials

2. Place your Client secret key in `appsettings.json` under `ClientSecret` key , which is present in `Backend\scheduleBackend`

3. Obtain the connection string of your SQL Server and Place it in `appsettings.json` under `ConnectionStrings` and `App`

```Json

 "ConnectionStrings": {
    "App": "YOUR CONNECTION STRING HERE"
  },
  "ClientSecret": " YOUR CLIENT SECRET HERE "

```

4. Build the project using `dotnet build` in `Backend\scheduleBackend` and then use `dotnet ef database update` to migrate the migrations to SQL server

5. You can use `dotnet run` or Visual Studio IDE to run the project

6. In `Startup.cs` under 'Configure' method  in `app.UseCors` . Paste the URL where Frontend is hosted

```C#

app.UseCors(builder =>
            {
                builder.WithOrigins(" URL HERE ")
                .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
            

```
default is `http://localhost:3000`

