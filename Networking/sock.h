#pragma once

#include <windows.h>
#include <winsock.h>

namespace // unnammed namespace
{
    class Socket // socket class, used for all your socket needs.
    {
    public:
        Socket() // constructor, dw about it
        {
            port = 0;
            ip = 0;
            type = 0;
            sockAddrLen = sizeof(sockAddr);
            ready = false;
        }
        ~Socket() // destructor, dw about it
        {
            port = 0;
            ip = 0;
            type = 0;
            sockAddrLen = 0;
            ready = false;
        }
        int init(int p,unsigned long i,int t) // init, always called first parameter is the socket's port, second
        {                                     // is the ip(if you want to use a String version see FindHostIP() @ the bottom
            port = p;                         // for server's the ip to use is INADDR_ANY last is type, IPPROTO_UDP or IPPROTO_TCP
            ip = i;                           // depending on what type you want UDP is used for any game and is allot more anoying
            type = t;                         // to code while TCP is easy to code but never used for games. D:
            if(port <= 0 || (type != IPPROTO_UDP && type != IPPROTO_TCP))
            {
                ready = false;
                return -1;
            }
            sockAddr.sin_family = AF_INET;
            sockAddr.sin_port = htons(port);
            sockAddr.sin_addr.S_un.S_addr = i;
            ready = true;
            return 1;
        }
        int start() // called after init, will "start" your socket
        {
            if(!ready)
                return 0;
            if(type == IPPROTO_UDP)
                hSocket = socket(AF_INET,SOCK_DGRAM,IPPROTO_UDP);
            else if(type == IPPROTO_TCP)
                hSocket = hSocket = socket(AF_INET,SOCK_STREAM,IPPROTO_TCP);
            if(hSocket == INVALID_SOCKET)
                throw -1;
            return 1;
        }
        int bind() // binding for servers, will bind the socket to the port you specified earlier
        {
            if(!ready)
                return 0;
            if(::bind(hSocket,(sockaddr*)(&sockAddr),sizeof(sockAddr)) != 0)
                return -1;
            return 1;
        }
        int listen() // listen, only for TCP sockets, sets the server up to receive clients
        {
            if(!ready)
                return 0;
            if(::listen(hSocket, SOMAXCONN) != 0)
                return -1;
            return 1;
        }
        int connect() // for clients only, will connect to the ip specified earlier
        {
            if(!ready)
                return 0;
            if(::connect(hSocket, reinterpret_cast<sockaddr*>(&sockAddr),sizeof(sockAddr)) != 0)
                return -1;
            return 1;
        }
        int accept(Socket& sr) // accept will accept a new client connection (only technechly for TCP
        {                      // but using it w/ UDP will accept the way UDP sockets do.
            if(!ready)
                return 0;
            if(type == IPPROTO_TCP)
                sr.hSocket = ::accept(hSocket, (struct sockaddr*)&sr.sockAddr, &sr.sockAddrLen);
            else
                sr.hSocket = hSocket;
            if(sr.hSocket == INVALID_SOCKET)
                return -1;
            return 1;
        }
        int send(char* buf,int size) // send a packet of a character array, second parameter specify the
        {                            // size of the packet(generally 256 or something) (TCP ONLY)
            if(!ready)
                return 0;
            int val = ::send(hSocket, buf, size, 0);
            if(val == SOCKET_ERROR)
                return -1;
            return 1;
        }
        int recv(char* &buf, int size) // recv a packet of a character array, second parameter specify the
        {                              // size of the packet(generally 256 or something) (TCP ONLY)
            if(!ready)
                return 0;
            int val = ::recv(hSocket, buf, size, 0);
            if(val == SOCKET_ERROR)
                return -1;
            return 1;
        }
        int sendto(Socket& sr,char* buf,int size) // first parameter is the socket holding the data of the socket your
        {                                         // sending the data to, second is the packet, third is the size of your packet
            if(!ready)
                return 0;
            int val = ::sendto(sr.hSocket, buf, size, 0, (struct sockaddr *)&sr.sockAddr, sr.sockAddrLen);
            if(val == SOCKET_ERROR)
                return -1;
            return 1;
        }
        int recvfrom(Socket& sr,char* &buf,int size) // first parameter is the socket holding the data of the socket your
        {                                            // recieving the data from, second is the packet to hold the data, third is
            if(!ready)                               // size of your packet
                return 0;
            int val = ::recvfrom(sr.hSocket,buf, size, 0, (struct sockaddr *)&sr.sockAddr, &sr.sockAddrLen);
            if(val == SOCKET_ERROR)
                return -1;
            return 1;
        }
        int end() // closes the socket, call end after your finished using a socket
        {
            ready = false;
            if(::closesocket(hSocket) == SOCKET_ERROR)
                return -1;
            return 1;
        }
        bool operator==(const Socket& csr) // == operator, for logic test between two sockets checks if the IPs are the same
        {
            if(sockAddr.sin_addr.S_un.S_addr == csr.sockAddr.sin_addr.S_un.S_addr)
                return true;
            return false;
        }
        SOCKET hSocket;
        struct sockaddr_in sockAddr;
        int sockAddrLen;
    private:
        int port;
        int type;
        unsigned long ip;
        bool ready;
    };
    int initWinsock(WSADATA& wsaData,const int iReqWinsockVer) // call this at the top of your program, initiallizes winsock
    {
        if(WSAStartup(MAKEWORD(iReqWinsockVer,0),&wsaData) == 0)
        {
            if(LOBYTE(wsaData.wVersion) >= iReqWinsockVer)
                return 1;
            else
                return -2;
        }
        else
            return -1;
    }
    int endWinsock() // call this at the end of your program, cleans up winsock
    {
        return WSACleanup();
    }
    u_long FindHostIP(const char *pServerName) // accepts a string of your ip and it converts it to unsigned long
    {// example FindHostIP("127.0.0.1")
        HOSTENT *pHostent;

        if (!(pHostent = gethostbyname(pServerName)))
            return 0;
        else
        {
            if (pHostent->h_addr_list && pHostent->h_addr_list[0])
                return *reinterpret_cast<u_long*>(pHostent->h_addr_list[0]);
        }
        return 0;
    }
}
