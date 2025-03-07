from typing import Protocol


class CanWalk(Protocol):
    def walk(self) -> None:
        pass


class CanQuack(Protocol):
    def quack(self) -> None:
        pass


class CanBark(Protocol):
    def bark(self) -> None:
        pass


class HasEyes(Protocol):
    def see(self) -> None:
        pass


class CanFly(Protocol):
    def fly(self) -> None:
        pass


class CanSwim(Protocol):
    def swim(self) -> None:
        pass


class Duck:
    def quack(self) -> None:
        print("Quack, quack!")

    def walk(self) -> None:
        print("Walks like a duck.")


class Dog:
    def walk(self) -> None:
        print("Walks like a dog.")

    def bark(self) -> None:
        print("Woof!")


def walk(animal: CanWalk) -> None:
    animal.walk()


def bark(animal: CanBark) -> None:
    animal.bark()


def quack(animal: CanQuack) -> None:
    animal.quack()


walk(Duck())
quack(Duck())
quack(Dog())
