# Blog API

## Overview
We are building a simple Blog using .Net and EFCore as well as JWT Token for Authentification

## Features
** User Creation and Authentification (Login) using JWT Tokens
**Create, Update, Publish, and Unpublish our blogs**
**Soft Delete** (this will keep the mhidden but still in the system just in case the FBI needs)
**Search Blogs** (by different categories)

## Endpoints
# User
    - `POST /User/CreateUser`
    - `POST /User/Login`
    - `GET /User/GetUserInfo`

# Blog
    - `POST /Blog/AddBlog`
    - `GET /Blog/GetAllBlogs`
    - `PUT /Blog/EditBlog`
    - `GET /Blog/GetBlogbyUserId/{userId}`
    - `DELETE /Blog/DeleteBlog`
    - `GET /Blog/GetBlogByCategory/{category}`

## Async Methods in C#

are used when queries databases without blocking our main thread

this leads to better understand perofrmances *Allows us to handle more than one request*