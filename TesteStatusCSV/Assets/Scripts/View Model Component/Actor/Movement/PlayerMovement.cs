using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    float MOV, range;
    float time, damage;
    Stats myStats;
    GameObject target;
    InventoryManager invManager;

    void Start()
    {
        myStats = GetComponentInParent<Stats>();
        MOV = myStats[StatTypes.MOV];
        range = myStats[StatTypes.RANGE];

        invManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }


    void FixedUpdate()
    {
        target = null;
        target = FindClosestEnemy(range);
        if (!target)
            FindClosestEnemy(Mathf.Infinity);
        if (!target)
            return;

        if (target)
        {
            float distance = (target.transform.position - transform.position).magnitude;
            Debug.DrawRay(transform.position, target.transform.position - transform.position, (distance <= range)? Color.green : Color.red, 0f);

            if (distance > range)
                Move(target);
            else Attack(target);
        }

    }

    GameObject FindClosestEnemy(float range)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        if (gos != null)
        {
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance && diff.y < range)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            return closest;
        }
        else
        {
            Debug.Log("Inimigo nao encontrado");
            return null;
        }

            
    }

    void Move (GameObject target)
    {
        Vector3 posDif = target.transform.position - transform.position;
        transform.Translate(new Vector2(Mathf.Sign(posDif.x),0) * MOV * Time.deltaTime);
    }

    void Attack(GameObject target)
    {
        damage = 0;
        if (Time.time >= time)
        {
            float delay = 1f / myStats[StatTypes.ASpeed];
            time = Time.time + delay;

            MagicFeature mFeature = GetMagicType( this.gameObject.transform.parent.gameObject );
            switch (mFeature.magicType) //Adicionar Status ou dano extra Physical
            {
                case MagicType.None:
                    //Debug.Log("Nenhum tipo magico");
                    break;
                case MagicType.Physical:
                    damage = mFeature.magicPower;
                    //Debug.Log("Physical added: " + damage);
                    break;
                case MagicType.Fire:
                    Add<FireStatusEffect>(target, mFeature.duration, mFeature.magicPower);
                    //Debug.Log("Fire effect with power: " + mFeature.magicPower + ", duration: " + mFeature.duration);
                    break;
                case MagicType.Ice:
                    //Add<FireStatusEffect>(target, mFeature.duration, mFeature.magicPower);                            ADICIONAR ICE STATUS EFFECT
                   // Debug.Log("Ice effect with power: " + mFeature.magicPower + ", duration: " + mFeature.duration);
                    break;
                case MagicType.Poison:
                    Add<PoisonStatusEffect>(target, mFeature.duration, mFeature.magicPower);
                    //Debug.Log("Poison effect with power: " + mFeature.magicPower + ", duration: " + mFeature.duration);
                    break;
                default:
                    //Debug.Log("Erro tipo magico");
                    return;
            }
            damage += myStats[StatTypes.DMG];
            int damageToEnemy = DamageDealt(damage, target);

            this.target.GetComponent<Stats>()[StatTypes.HP] -= damageToEnemy;
            //Debug.Log("Attack before RES:" + damage + ", after RES: " + damageToEnemy);
        }

        //if (GetComponent<Rigidbody2D>().velocity.y == 0)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1000f, 1000f), Random.Range(0, 400f)));
        //    Debug.Log("Jumped");
        //}
    }

    MagicFeature GetMagicType(GameObject player)
    {
        EquipSlots slot = EquipSlots.Weapon;
        GameObject weapon = invManager.ItemInSlot(player, slot);
        return weapon.GetComponent<MagicFeature>();
    }

    void Add<T>(GameObject target, float duration, float statusPower) where T : StatusEffect
    {
        DurationStatusCondition condition = target.GetComponent<Status>().Add<T, DurationStatusCondition>(statusPower);
        condition.duration = duration;
    } //Add Status Effect

    int DamageDealt (float totalDamage, GameObject target)
    {
        float targetDEF = target.GetComponent<Stats>()[StatTypes.DEF];
        float resPercent = 100f / (100f + targetDEF);
        float _dmg = totalDamage * resPercent;
        return Mathf.FloorToInt(_dmg);
    }
}