package main

import (
	"fmt"
)

func main() {
	bank := Bank{}
	var choice int

	fmt.Println("=== Консольный Банк на Go v1.0 ===")

	for {
		fmt.Println("\n1. Регистрация\n2. Открыть счет\n3. Вход\n4. Проверить сумму\n0. Выход")
		fmt.Print("> ")
		fmt.Scanln(&choice)

		if choice == 0 {
			break
		}

		switch choice {
		case 1:
			var name, cont string
			var age int
			fmt.Print("Имя: ")
			fmt.Scanln(&name)
			fmt.Print("Возраст: ")
			fmt.Scanln(&age)
			fmt.Print("Контакты: ")
			fmt.Scanln(&cont)
			bank.AddClient(name, age, cont)

		case 2:
			var name string
			fmt.Print("Имя клиента: ")
			fmt.Scanln(&name)
			bank.OpenAccount(name)

		case 3:
			var name, pass string
			fmt.Print("Имя: ")
			fmt.Scanln(&name)
			fmt.Print("Пароль: ")
			fmt.Scanln(&pass)
			bank.Authenticate(name, pass)

		case 4:
			var amount float64
			fmt.Print("Сумма: ")
			fmt.Scanln(&amount)
			if amount > 100000 {
				fmt.Println("[ALARM] Подозрительно много!")
			} else {
				fmt.Println("[Ок] Все хорошо.")
			}

		default:
			fmt.Println("Неверный выбор")
		}
	}
}
