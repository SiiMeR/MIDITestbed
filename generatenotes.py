
notes = ["A", "ASharp", "B", "C", "CSharp", "D", "DSharp", "E", "F", "FSharp", "G", "GSharp"]

for i in range(21, 109):
        n = ((i-9) % 12)
        m = ((i-9) // 12)
        print(str(notes[n]) + str(m-1) + " = " + str(i) + ",")

    
