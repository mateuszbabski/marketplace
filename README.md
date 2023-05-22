# Marketplace

## Table of content:
* [Project description](#project-description)
* [Technologies](#technologies)
* [Setup](#setup)
* [Features](#features)
* [Architecture](#architecture)
* [Contribution](#contribution)


## Project description
Marketplace works like a small business aggregator. Customer is able to order different products from different shops, but he gets one order package. 

To implement:

- diagrams
- clear description

## Technologies

- C# 11
- .Net 7
- EntityFrameworkCore 7.0.4
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
- Adding, updating and deleting products available to buy
- Creating shopping cart. Adding, removing products and deleting cart
- Placing and cancelling orders
- Splitting order by shops
- Creating invoice for customer and split them for shops
- Currency conversion while adding to cart various products

To implement:
- Change shopping cart currency
- Autoupdate prices while checking out cart/placing order basis on current rates
- Searching products
- Notify all shops owners about products to prepare
- Sending invoices
- Sending email notifications about orders
- Confirm account, forgot/reset password features
- Unit tests

## Architecture

Projects is based on Clean architecture principles, following DDD approach. CQRS and MediatR handle application features.

## Contribution

Feel free to fork project and work on it with me. I am open to any suggestions, pull requests just to make project better.
