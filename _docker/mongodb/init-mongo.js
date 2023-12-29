db.createUser(
    {
        user  : "notification",
        pwd   : "pwd",
        roles : [
            {
                role : "readWrite",
                db   : "notification-db"
            }
        ]
    }
)