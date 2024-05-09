
# Banking System

## Project status
![workflow](https://github.com/SharbelKH/banking-system/actions/workflows/dotnet.yml/badge.svg) [![Unit test](https://github.com/SharbelKH/banking-system/actions/workflows/code-coverage.yml/badge.svg)](https://github.com/SharbelKH/banking-system/actions/workflows/code-coverage.yml)

## Project description
We will design a banking system that manages users and their different bank accounts.
The functionality of the system will include the following: create users, perform transactions, manage accounts, generate account activity history.
The banking system shall keep track of the users and their accounts, also it will surveil for malificent activity for safety reason simulating real world scenario. (Such as abnormaly large withdrawal from an account).

### 1. User Interface:
- Registration and login for customers and admin.
- Authentication for login for security.
- Profile management to update personal information.
### 2. Account Interface:
- Create different accounts such as checkings, savings etc.
- Account history.
### 3. Transactions:
- Support of transactions such as withdrawal, transfers between accounts, deposits etc.
- Transaction History.
- Transaction authentication with error handling.
### 4. Banking System Administration:
- Manage users, accounts and transactions.
- Monitor suspitious activity


### Compilation and running instructions
We are using a local database for our project using MySQL.
Next to the README file we provide a script for creating a database which will be need to initialized on your local server.
Navigate to the BankApplication.csproj file. 
How to compile: write 'dotnet build' command in .NET CLI
How to run: write 'dotnet run'

### Test and CLI instructions for running tests
Navigate to the BankApplication.csproj file.
How to run tests: write 'dotnet test'
This will run the tests that are available for the project

### Instructions on how to run the linter
Whenever there is a push or pull request, the super linter will run in GitHub automatically
but if you want to run it manually, you can enter these 2 commands in a shell on your computer if you have a docker installed: 
FIRST:
docker pull github/super-linter:v5.7.2

-----------------------------------------
SECOND:

docker run --rm \
    --volume "$(pwd):/tmp/lint" \
    --workdir /tmp/lint \
    -e DEFAULT_BRANCH=main \
    -e GITHUB_TOKEN=YOUR_GITHUB_TOKEN \           --Replace YOUR_GITHUB_TOKEN with a personal access token that has the necessary permissions to access your repository. You can generate a personal access token in your GitHub account settings.
    github/super-linter:v5.7.2
    
-----------------------------------------
If that doesnt work, you can try to run: 
docker run \
  -e LOG_LEVEL=DEBUG \
  -e RUN_LOCAL=true \
  -v /path/to/local/codebase:/tmp/lint \
  --rm \
  ghcr.io/super-linter/super-linter:latest
  
-----------------------------------------
### Built with
[Material Design In XAML](http://materialdesigninxaml.net/) - Fully open source and one of the most popular GUI libraries for WPF.

### Run local database instruction
1. make sure sql server is installed on you local computer
    - if not here is the link: https://www.microsoft.com/en-us/sql-server/sql-server-downloads 
2. open the project
3. under solution explorer, there will be a .mdf file, double click on the file. This will run the database on your local computer.
4. under View -> server expoler you'll find banksystem_database, right click on the database and click on properties.
5. copy the connection link, important to get the entire link.
6. naviagte to OurSqlConnectionString.cs and add your personal link.
7. save and run the projects, to make sure that the connection is working, log in with Phone Number: 123456789 and password: Groda123. You will receive a message saying the log in was successful, this means the connection is working.

 ### code coverage
1. Open your C# solution in Visual Studio.
2. Ensure your unit tests are running properly.
3. Go to Test > Analyze Code Coverage > For All Tests.
4. After the tests complete, the code coverage report will open, showing the percentage of code covered by the tests.
   NOTE! You can also find code coverage metrics in github actions under 'Code coverage artifact' workflow. Select a specific run to analyse, download 'code-coverage-report zip and extract. The report can be found in 'index.html'.

## Group members
- Sherbal Al khouri, SharbelKH
- Rebecca Huynh, Rebeccahuynh0
- Erik Wöhry, ErikWilhelmWohry
- Emil Fröding, Froding0
- Jakob Skoglund, JakobSkoglund

## Specifications
A banking system with a GIU will be implemented in C# for backend and WPF for frontend. A data base for customer data will be implemented in MySQL. The project will rely heavily on encapsulation and other object orientated principles. 

## Declaration section
I, Rebecca Huynh, declare that I am the sole author of the content I add to this repository 

I, Sherbal Al khouri, declare that I am the sole author of the content I add to this repository 

I, Emil Fröding, declare that I am the sole author of the content I add to this repository 

I, Jakob Skoglund, declare that I am the sole author of the content I add to this repository

I, Erik Wöhry, declare that I am the sole author of the content I add to this repository 

# Our kanban board
[Link to kanban board](https://github.com/users/SharbelKH/projects/1/views/1)

