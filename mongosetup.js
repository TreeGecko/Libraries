
conn = new Mongo();
db = conn.getDB("admin");

db.createUser( { user: "tg_test_user",  pwd: "testing", roles: [ { role: "readWrite", db: "treegecko_test" } ] } )