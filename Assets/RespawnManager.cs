using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public List<GameObject> MobPool = new List<GameObject>();   //게임에서 미리 생성될(활성화/비활성화 될) Mob들의 리스트
    public GameObject[] Mobs;   //Mob의 prefabs들을 넣는 배열
    public int objCnt = 1;
    void Awake() {          //MobPool 리스트에 생성할 Mob들을 objCnt개 만큼 추가함
        for(int i=0; i<Mobs.Length; i++){
            for(int q=0; q<objCnt; q++){
                MobPool.Add(CreateObj(Mobs[i], transform));
            }
        }
    }
    private void Start(){
        GameManager.instance.onPlay += PlayGame;
    }


    void PlayGame(bool isplay){
        if(isplay){
            for(int i=0; i<MobPool.Count; i++){
                if(MobPool[i].activeSelf){
                    MobPool[i].SetActive(false);
                }
            }
            StartCoroutine(CreateMob());
        }
        else{
            StopAllCoroutines();
        }
            
    }
    //몇개 생성되다가 마는거 해결해야함
    IEnumerator CreateMob(){    // MobPool리스트에 있는 랜덤한 몹을 정해진 시간마다 한개씩 활성화
        yield return new WaitForSeconds(0.5f);
        
        while(GameManager.instance.isPlay){
            if(!GameManager.instance.isEmptySpaceSpawn){
                MobPool[DeactiveMob()].SetActive(true);
            }
            yield return new WaitForSeconds(Random.Range(2f, 3f));
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
    
    GameObject CreateObj(GameObject obj, Transform parent){     //새로 만든 오브젝트를 비활성화해서 반환
        GameObject copy = Instantiate(obj);
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}
