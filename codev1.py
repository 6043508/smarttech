# code van microbit naar normale python
# testen of de code uperhaupt werkt
# shows which led is turned on and why

def on_forever():
    # sensor.measure()
    temp = 17
    humidity = 61

    if temp < 10 or temp >15:  #placeholder values
        print("temp is fine")
        print("so red led is turned off")

        # code for turning on heating

    else:
        print("temp is not fine")
        print("turned green led off")

        # code for turning off heating

    if humidity < 30 or humidity >60: #placeholder values
        print("humidity is fine")
        print("red_led_off_humid")

        # code for turning on sprayer

    else:
        print("humidity is not fine")
        print("green_led_off_humid")

        # code for turning off heating

on_forever()








