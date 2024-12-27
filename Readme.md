TMS - stand for "Task Management System"

To start application do following steps

```
cd Intaker.TMS
docker-compose up
cd Intaker.TMS.Api
dotnet run
cd ../Intaker.TMS.Worker
dotnet run
```
Then go to http://localhost:5194/swagger/index.html

There is already two task in db, with id 1 and 2.
