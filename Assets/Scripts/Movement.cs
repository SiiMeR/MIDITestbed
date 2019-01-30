using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;


public enum KeyMapping
{
    A0 = 21,
    ASharp0 = 22,
    B0 = 23,
    C0 = 24,
    CSharp0 = 25,
    D0 = 26,
    DSharp0 = 27,
    E0 = 28,
    F0 = 29,
    FSharp0 = 30,
    G0 = 31,
    GSharp0 = 32,
    A1 = 33,
    ASharp1 = 34,
    B1 = 35,
    C1 = 36,
    CSharp1 = 37,
    D1 = 38,
    DSharp1 = 39,
    E1 = 40,
    F1 = 41,
    FSharp1 = 42,
    G1 = 43,
    GSharp1 = 44,
    A2 = 45,
    ASharp2 = 46,
    B2 = 47,
    C2 = 48,
    CSharp2 = 49,
    D2 = 50,
    DSharp2 = 51,
    E2 = 52,
    F2 = 53,
    FSharp2 = 54,
    G2 = 55,
    GSharp2 = 56,
    A3 = 57,
    ASharp3 = 58,
    B3 = 59,
    C3 = 60,
    CSharp3 = 61,
    D3 = 62,
    DSharp3 = 63,
    E3 = 64,
    F3 = 65,
    FSharp3 = 66,
    G3 = 67,
    GSharp3 = 68,
    A4 = 69,
    ASharp4 = 70,
    B4 = 71,
    C4 = 72,
    CSharp4 = 73,
    D4 = 74,
    DSharp4 = 75,
    E4 = 76,
    F4 = 77,
    FSharp4 = 78,
    G4 = 79,
    GSharp4 = 80,
    A5 = 81,
    ASharp5 = 82,
    B5 = 83,
    C5 = 84,
    CSharp5 = 85,
    D5 = 86,
    DSharp5 = 87,
    E5 = 88,
    F5 = 89,
    FSharp5 = 90,
    G5 = 91,
    GSharp5 = 92,
    A6 = 93,
    ASharp6 = 94,
    B6 = 95,
    C6 = 96,
    CSharp6 = 97,
    D6 = 98,
    DSharp6 = 99,
    E6 = 100,
    F6 = 101,
    FSharp6 = 102,
    G6 = 103,
    GSharp6 = 104,
    A7 = 105,
    ASharp7 = 106,
    B7 = 107,
    C7 = 108
}
public class Movement : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public float moveSpeed = 10.0f;
    public bool isGrounded;
    private Rigidbody2D _rigidbody2D;

    public List<KeyMapping> moveSequence;
    public int currentKey = 0;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        isGrounded = true;
    }

    void Update()
    {
        
        var nextIdx =  Wrap(currentKey + 1, moveSequence.Count);
        var lastIdx = Wrap(currentKey - 1, moveSequence.Count);

        print(nextIdx +  " " + lastIdx);
        var nextKey = moveSequence[nextIdx];
        var prevKey = moveSequence[lastIdx];
        
        if (GetKeyDown(nextKey))
        {
            _rigidbody2D.AddForce(Vector3.right * moveSpeed  * Time.deltaTime, ForceMode2D.Impulse);
            currentKey++;

        }
        
        if (GetKeyDown(prevKey))
        {
            _rigidbody2D.AddForce(Vector3.left * moveSpeed  * Time.deltaTime, ForceMode2D.Impulse);
            currentKey--;
        }


        if (GetKeyDown(KeyMapping.C4) )
        {
            _rigidbody2D.AddForce(Vector2.up * Time.deltaTime * jumpForce *  GetKeyStrength(KeyMapping.C4), ForceMode2D.Impulse);
            isGrounded = false;
        }
        
    }

    public float GetKeyStrength(KeyMapping key)
    {
        return MidiMaster.GetKey((int) key);
    }
    public bool GetKey(KeyMapping key)
    {
        return GetKeyStrength(key) > float.Epsilon;
    }

    public bool GetKeyDown(KeyMapping key)
    {
        return MidiMaster.GetKeyDown((int) key);
    }
    
    public static int Wrap(int index, int n)
    {
        return ((index % n) + n) % n;
    }
}