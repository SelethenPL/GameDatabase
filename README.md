# Gaming
Database project for purpouse of the 'Database Management Systems', University sub.

Application developed with MsSQL and ASP.Net Web Application.
Main idea is to perform a simple taks, like adding, editing, deleting new data.
Database is about simple MMO-RPG game, where you can plan data and maintain current data.

Tables:
* Account
* Hero
* Equipment
* NPC
* Quest
* Guild
* Place
* Raid
* Event
* Boss
Procedure:
* levelUpGuildMembers(INT @guildId, INT @amountOfLevels) -> level up every member of a given guild
Function:
* raidsOnTheGo() -> returns a table of all raids which are currently ongoing (dates)
