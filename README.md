# Marketplace

## Table of content:
* [Project description](#project-description)
* [Technologies](#technologies)
* [Setup](#setup)
* [Features](#features)
* [Architecture](#architecture)
* [Contribution](#contribution)


## Project description
Marketplace works like a small bussiness aggregator. Customer is able to order different products from different shops, but he gets one order package. 

To implement:

- diagrams
- clear description

## Technologies

- C# 11
- .Net 7
- EntityFrameworkCore 6.0.8
- MSQLServer

## Setup

#### Clone to repository
```
$ git clone https://github.com/mateuszbabski/marketplace
```

#### Go to the folder you cloned
```
$ cd marketplace
```

#### Install dependencies
```
$ dotnet restore
```

#### Update appsettings.json 

#### Create empty database, create migration and update database

#### Set API Layer as startup project

#### Run application
```
$ dotnet run
```

## Features

Done:
- Register/Login for Customer and Shop separately

To implement:
- Adding, updating and deleting products available to buy
- Searching products
- Placing orders
- Splitting order to notify all shops owners about products to prepare
- Creating and sending invoices
- Sending email notifications about orders
- Confirm account, forgot/reset password features
- Unit tests

## Architecture

Projects is based on Clean architecture principles, following DDD approach. CQRS and MediatR handle application features.

## Contribution

Feel free to fork project and work on it with me. I am open to any suggestions, pull requests just to make project better.
