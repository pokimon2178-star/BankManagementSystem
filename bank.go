package main

import (
	"fmt"
	"math/rand"
	"strings"
)

type Bank struct {
	Clients []Client
}

func (b *Bank) AddClient(name string, age int, contacts string) {
	client, err := NewClient(name, age, contacts)
	if err != nil {
		fmt.Printf("[Ошибка] %v\n", err)
		return
	}
	b.Clients = append(b.Clients, client)
	fmt.Printf("[Система] Клиент %s успешно добавлен.\n", name)
}

func (b *Bank) Authenticate(name string, password string) bool {
	for i := range b.Clients {
		if strings.ToLower(b.Clients[i].FullName) == strings.ToLower(name) {

			if b.Clients[i].Status == "Blocked" {
				fmt.Println("[Защита] Аккаунт заблокирован.")
				return false
			}

			if password == "1234" {
				b.Clients[i].FailedLoginAttempts = 0
				fmt.Printf("[Доступ] Добро пожаловать, %s!\n", name)
				return true
			}

			b.Clients[i].FailedLoginAttempts++
			fmt.Printf("[Внимание] Неверный пароль. Осталось попыток: %d\n", 3-b.Clients[i].FailedLoginAttempts)

			if b.Clients[i].FailedLoginAttempts >= 3 {
				b.Clients[i].Status = "Blocked"
				fmt.Println("[КРИТИЧНО] Аккаунт ЗАБЛОКИРОВАН.")
			}
			return false
		}
	}
	fmt.Println("[Ошибка] Клиент не найден.")
	return false
}

func (b *Bank) OpenAccount(name string) {
	for i := range b.Clients {
		if strings.ToLower(b.Clients[i].FullName) == strings.ToLower(name) {
			newAcc := fmt.Sprintf("ACC-%d", rand.Intn(899)+100)
			b.Clients[i].AccountNumbers = append(b.Clients[i].AccountNumbers, newAcc)
			fmt.Printf("[Счета] Открыт счет %s для %s\n", newAcc, name)
			return
		}
	}
}
