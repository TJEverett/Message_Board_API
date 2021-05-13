# Message Board API

#### _Track Messages, 05/13/2021_

#### By _**Tristen Everett**_

## Description

This project was an attempt at practicing the skills I am learning to program in C# to create an API. In this project I built an .NET API that allows users to interact with a database of messages and the groups that they belong to. This project was built to meet the needs of the user stories listed below.

### User Stories

* As a user, I want to be able to GET all messages related to a specific group.
* As a user, I want to be able to POST messages to a specific group.
* As a user, I want to be able to see a list of all groups.
* As a user, I want to input date parameters and retrieve only messages posted during that timeframe.
* As a user, I want to be able to PUT and DELETE messages, but only if I wrote them. (Start by requiring a user_name param to match the user_name of the author on the message. You can always try authentication later.)

## Setup/Installation Requirements

1. Clone this Repo
2. Run `dotnet ef database update` from within /MessageBoard to create the database
3. You may need to update the file /MessageBoard/appsettings.json to match the userID and password for the computer you're using
4. Run `dotnet restore` from within /MessageBoard file location
5. Run `dotnet build` from within /MessageBoard file location
6. Run `dotnet run` from within /MessageBoard file location

### API Endpoints

* `api/groups` (GET) - returns all groups in the database
* `api/groups` (POST) - adds a group to the database
   * Body: `{"name": (string)}`
* `api/groups/{int id}` (GET) - returns a specific group from the database
* `api/messages` (GET) - returns a list of messages from the database, can be narrowed by using additional route parameters
   * `username=(string)`
   * `groupid=(int)`
   * `afterdate=(DateTime)`
   * `beforedate=(DateTime)`
* `api/messages` (POST) - adds a message to the database
   * Body: `{"username": (string), "content": (string), "groupid": (int)}`
* `api/messages/{int id}` (GET) - return a specific message from the database
* `api/messages/{int id}` (PUT) - update a specific message in the database if usernames match
   * Body: `{"username": (string), "content": (string), "groupid": (int)}`
* `api/messages/{int id}` (DELETE) - delete a specific message from the database if usernames match
   * Body: `(string username)`

## Technologies Used

* C#
* ASP.NET Core
* Entity Framework Core
* MYSQL

### License

This software is licensed under the MIT license

Copyright (c) 2021 **_Tristen Everett_**