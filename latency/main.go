package main

import (
	"fmt"
	"github.com/go-mangos/mangos"
	"github.com/go-mangos/mangos/protocol/pull"
	"github.com/go-mangos/mangos/protocol/push"
	"github.com/go-mangos/mangos/transport/tcp"
	"os"
	"time"
	"strconv"
)

type Node struct {
	name     string
	push     string
	pull     string
	pullSock mangos.Socket
	pushSock mangos.Socket
	count    uint64
}

func (node *Node) Start() {
	var err error

	if node.pullSock, err = pull.NewSocket(); err != nil {
		die("can't get new pull socket: %s", err)
	}
	node.pullSock.AddTransport(tcp.NewTransport())
	if err = node.pullSock.Listen(node.pull); err != nil {
		die("can't listen on pull socket: %s", err.Error())
	}
	go node.Listen()
}

func (node *Node) Connect() {
	if node.push == "" {
		return
	}
	var err error

	if node.pushSock, err = push.NewSocket(); err != nil {
		die("can't get new push socket: %s", err.Error())
	}
	node.pushSock.AddTransport(tcp.NewTransport())
	if err = node.pushSock.Dial(node.push); err != nil {
		die("can't dial on push socket: %s", err.Error())
	}
}

func (node *Node) Send(msg [] byte) {
	if node.push == "" {
		start, _ := strconv.ParseInt(string(msg), 10, 64)
		time.Now().UnixNano()
		fmt.Printf("chain end in %d nanosec\n", time.Now().UnixNano()-start)
		return
	}
	var err error
	if err = node.pushSock.Send([]byte(msg)); err != nil {
		die("can't send message on push socket: %s", err.Error())
	}
}

func (node *Node) Listen() {
	var msg [] byte

	for {
		msg, _ = node.pullSock.Recv()
		node.Send(msg)
	}
}

func die(format string, v ...interface{}) {
	fmt.Fprintln(os.Stderr, fmt.Sprintf(format, v...))
	os.Exit(1)
}

func main() {
	const n = 1000
	var nodes [n]Node

	for i := 0; i < n; i++ {
		nodes[i].name = fmt.Sprintf("node%d", i)
		nodes[i].pull = fmt.Sprintf("tcp://127.0.0.1:%d", 30000+i)
		nodes[i].push = fmt.Sprintf("tcp://127.0.0.1:%d", 30000+i+1)
		nodes[i].Start()
		if i == n-1 {
			nodes[i].push = ""
		}

	}

	for i := 0; i < n; i++ {
		nodes[i].Connect()
	}

	for i := 0; i < 10; i++ {
		var msg = fmt.Sprintf("%d", time.Now().UnixNano())
		nodes[0].Send([]byte(msg))
	}
	time.Sleep(time.Second * 10)
}
