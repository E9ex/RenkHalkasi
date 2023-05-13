using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalkaControl : MonoBehaviour
{
   public float donmeHizi;
   public bool SolaDon = true;


   private void FixedUpdate()
   {
      if (SolaDon)
      {
         transform.Rotate(0f,0f,donmeHizi*Time.deltaTime);
      }
      else
      {
         transform.Rotate(0f,0f,-donmeHizi*Time.deltaTime);
      }
   }
}
