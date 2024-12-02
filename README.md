# Orleans Demo Project
A application to learn about Microsoft Orleans and test out various scenarios. I keep hearing how Orleans is one of the underrated frameworks from Microsoft.  I want to see if Orleans could be a good fit to replace how I've done things.

## Description
This project is a basic web api, with some basic CQRS functionality, to see how Orleans might work with a typical api.  .NET Aspire has been added to the mix as this is a fantastic framework for setting up infrastructure for your applications.

The project will have various domains - for example, blog posts, users, etc.  These represent domains I typically deal with.

## Features and Functionality to Test

Below is a list of features and functionality that I typically use in the projects I build. 

| Feature/Functionality | Capable |
|-----------|:-----------:|
| Basic CRUD (Create, Read *(GetById)*, Update, Delete) | - |
| Create records with unique constraints (e.g. Users must have unique email address) | - |
| Add DAL for 'GetAll<some_item>' (including joins) | - |
| Validation on CRUD operations | - |
| Caching | - |
| Integration with external services | - |

