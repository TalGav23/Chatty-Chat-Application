from socket import *
from threading import Thread
import mysql.connector


def Clients(client1, client2, isMain):
    c1 = False
    c2 = False
    isSignin = False
    while True:
        print("Waiting For Cient To Send Details...")
        data = client1.recv(1024).decode().split(";")
        if data[0] == "Login":
            Pointer.execute("Select * from AccountsDatabase where Username = %s and Password = %s", [data[1], data[2]])
            res = Pointer.fetchall()
            if res:
                for _ in res:
                    client1.sendall("Signed In Successfully".encode())
                    if isMain:
                        c1 = True
                    else:
                        c2 = True
                    break
                break
            else:
                client1.sendall("Sign In Failed".encode())
        if data[0] == "SignUp":
            if data[1] == "":
                client1.sendall("User Empty".encode())
            elif data[1] == "":
                client1.sendall("Pass Empty".encode())
            else:
                Pointer.execute("Select * from AccountsDatabase")
                res = Pointer.fetchall()
                if len(res) != 0:
                    for _ in res:
                        if data[1] in _[0]:
                            client1.sendall("User Already Exist".encode())
                            isSignin = True
                            break
                    if not isSignin:
                        Pointer.execute("insert into AccountsDatabase values(%s,%s)", (data[1], data[2]))
                        mydb.commit()
                        client1.sendall("Account Created Successfully".encode())
    print("Connected " + str(isMain))
    while True:
        if c1 and c2:
            pass  # client2.sendall(str(data[1]).encode())


mydb = mysql.connector.connect(
    host="",
    user="",
    password="",
    database="",
)
Pointer = mydb.cursor()

server = socket(AF_INET, SOCK_STREAM)
server.bind(("", 6969))
server.listen(2)
print("Waiting for Client 1.....")
client1, addr1 = server.accept()
print("Client 1 Connected")
print("Waiting for Client 2.....")
client2, addr2 = server.accept()
print("Client 2 Connected")

t = Thread(target=Clients, args=(client2, client1, False,))
t.start()
Clients(client1, client2, True, )
