# code voor microbit
from microbit import *
from elecfreaks import Led
import dht

#assigns leds to variables
groeneled = Led(pin1)
rodeled = Led(pin3)
sensor = dht.DHT11(machine.Pin(4))


def on_forever():
    sensor.measure()
    temp = sensor.temperature()
    humidity = sensor.humidity()

    if temp < 10 or temp >15:  #placeholder values
        groeneled.on()
        rodeled.off()
        #turn on heating

    else:
        rodeled.on()
        groeneled.off()
        #turn off heating

    if humidity < 30 or humidity >60: #placeholder values
        groeneled.on()
        rodeled.off()
        #turn on sprayer

    else:
        rodeled.on()
        groeneled.off()
        #turn off sprayer

while True:
    on_forever()
    sleep(1000)  #checkt elk sec








