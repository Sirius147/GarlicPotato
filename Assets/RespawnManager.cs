using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public List<GameObject> MobPool = new List<GameObject>();   //게임에서 미리 생성될(활성화/비활성화 될) Mob들의 리스트
    public GameObject[] Mobs;   //Mob의 prefabs들을 넣는 배열

    public List<GameObject> ItemPool = new List<GameObject>();  //게임에서 미리 생성될(활성화/비활성화 될) Item들의 리스트
    public GameObject[] Items;   //Item의 prefabs들을 넣는 배열

    public int mob_objCnt = 3;
    public int item_objCnt = 1;

    void Awake() {
        for(int i=0; i<Mobs.Length; i++){          //MobPool 리스트에 생성할 Mob들을 mob_objCnt개 만큼 추가함
            for(int q=0; q<mob_objCnt; q++){
                MobPool.Add(CreateObj(Mobs[i], transform));
            }
        }
        for(int i=0; i<Items.Length; i++){          //ItemPool 리스트에 생성할 Item들을 item_objCnt개 만큼 추가함
            for(int q=0; q<item_objCnt; q++){
                ItemPool.Add(CreateObj(Items[i], transform));
            }
        }
    }
    private void Start(){
        //GameManager.instance.onPlay += PlayGame;
        PlayGame(GameManager.instance.isPlay);
        //Debug.Log("respawnmanager start");
    }


    void PlayGame(bool isplay){
        if(isplay){
            for(int i=0; i<MobPool.Count; i++){
                if(MobPool[i].activeSelf){
                    MobPool[i].SetActive(false);
                }
            }
            for(int i=0; i<ItemPool.Count; i++){
                if(ItemPool[i].activeSelf){
                    ItemPool[i].SetActive(false);
                }
            }
            StartCoroutine(CreateMob());
        }
        else{
            StopAllCoroutines();
        }
            
    }
  
    IEnumerator CreateMob(){    // MobPool리스트에 있는 랜덤한 몹을 정해진 시간마다 한개씩 활성화
        yield return new WaitForSeconds(1f);
        
        while(GameManager.instance.isPlay){
            //int temp = Random.Range(1, 3);      //개발 편의성을 위해 아이템 많이 생성되게
            int temp = Random.Range(1, 15);
            if(!GameManager.instance.isHoleSpawn){
                if(temp != 1){
                    int dm = DeactiveMob();
                    MobPool[dm].SetActive(true);
                    if(dm == 9 || dm == 10 || dm == 11){    //Mob_3fork 나왔을 때 잠시 대기
                        yield return new WaitForSeconds(1f);
                    }
                } 
                else
                    ItemPool[DeactiveItem()].SetActive(true);
            }
            yield return new WaitForSeconds(Random.Range(0.8f, 1.6f));
        }   
    }

    int DeactiveMob(){      //지금 활성화되어 있지 않은 Mob을 찾는 함수
        List<int> num = new List<int>();
        for(int i=0; i<MobPool.Count; i++){
            if(!MobPool[i].activeSelf)
                num.Add(i);
        }
        int x=0;
        if(num.Count > 0)
            x = num[Random.Range(0, num.Count)];
        return x;
    }

    int DeactiveItem(){      //지금 활성화되어 있지 않은 Item을 찾는 함수
        List<int> num = new List<int>();
        for(int i=0; i<ItemPool.Count; i++){
            if(!ItemPool[i].activeSelf)
                num.Add(i);
        }
        int x=0;
        if(num.Count > 0)
            x = num[Random.Range(0, num.Count)];
        return x;
    }
    
    GameObject CreateObj(GameObject obj, Transform parent){     //새로 만든 오브젝트를 비활성화해서 반환
        GameObject copy = Instantiate(obj);
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}