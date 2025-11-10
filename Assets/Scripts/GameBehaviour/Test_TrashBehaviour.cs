using UnityEngine;


public class Test_TrashBehaviour : MonoBehaviour
{
    [Header("Despawn Settings")]
    public float maxYPos = -5;
    
    [Header("Trash Types")]
    [SerializeField] private TrashType currentTrashType = TrashType.None;
    
    [Header("Trash Models")]
    [SerializeField] private GameObject plasticModel;
    [SerializeField] private GameObject glassModel;
    [SerializeField] private GameObject paperModel;
    [SerializeField] private GameObject metalModel;
    [SerializeField] private GameObject organicModel;
    
    private void Awake()
    {
        DeactivateAllModels();
    }
    
    void OnEnable()
    {
        SetRandomTrashType();
    }

    void Update()
    {
        if(maxYPos > transform.position.y) 
        {
            gameObject.SetActive(false);
        }
    }
    

    private void DeactivateAllModels()
    {
        if (plasticModel != null) plasticModel.SetActive(false);
        if (glassModel != null) glassModel.SetActive(false);
        if (paperModel != null) paperModel.SetActive(false);
        if (metalModel != null) metalModel.SetActive(false);
        if (organicModel != null) organicModel.SetActive(false);
    }
    
    public void SetRandomTrashType()
    {
        int randomType = Random.Range(1, 6);
        TrashType newType = (TrashType)randomType;
        
        SetTrashType(newType);
    }
    
    public void SetTrashType(TrashType type)
    {
        if (type == currentTrashType) return;
        
        currentTrashType = type;
        UpdateModel();
    }
    
    private void UpdateModel()
    {
        DeactivateAllModels();
        
        if (currentTrashType == TrashType.None) return;
        
        switch (currentTrashType)
        {
            case TrashType.Plastic:
                if (plasticModel != null)
                {
                    plasticModel.SetActive(true);
                }
                break;
                
            case TrashType.Glass:
                if (glassModel != null)
                {
                    glassModel.SetActive(true);
                }
                break;
                
            case TrashType.Paper:
                if (paperModel != null)
                {
                    paperModel.SetActive(true);
                }
                break;
                
            case TrashType.Metal:
                if (metalModel != null)
                {
                    metalModel.SetActive(true);
                }
                break;
                
            case TrashType.Organic:
                if (organicModel != null)
                {
                    organicModel.SetActive(true);
                }
                break;
        }
    }
    
    public TrashType GetTrashType()
    {
        return currentTrashType;
    }
    
    public bool IsTrashType(TrashType type)
    {
        return currentTrashType == type;
    }
    
    void OnDisable()
    {
        DeactivateAllModels();
    }
}
