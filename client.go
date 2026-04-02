package main

import (
	"errors"
)

type Client struct {
	FullName            string
	Age                 int
	Contacts            string
	Status              string
	AccountNumbers      []string
	FailedLoginAttempts int
}

func NewClient(name string, age int, contacts string) (Client, error) {
	if age < 18 {
		return Client{}, errors.New("клиент должен быть старше 18 лет")
	}

	return Client{
		FullName:       name,
		Age:            age,
		Contacts:       contacts,
		Status:         "Active",
		AccountNumbers: []string{},
	}, nil
}
