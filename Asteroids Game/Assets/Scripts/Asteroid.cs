using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;

    public float size = 1.0f;

    public float minSize = 0.5f;

    public float maxSize = 1.5f;

    public float speed = 50.0f;

    public float maxLifetime = 30.0f;

    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
    }

    /*
      Dòng code này đặt hướng di chuyển cho thiên thạch bằng cách thêm lực vào Rigidbody, 
      đồng thời hủy đối tượng sau một khoảng thời gian nhất định. 

      🔹 _rigidbody.AddForce(direction * this.speed);
        _rigidbody: Biến tham chiếu đến thành phần Rigidbody2D của thiên thạch.
        AddForce(...): Hàm của Unity dùng để áp dụng một lực lên vật thể.
        direction * this.speed:
        direction: Hướng di chuyển của thiên thạch (Vector2).
        this.speed: Tốc độ của thiên thạch.
        direction * this.speed: Tạo ra một lực có hướng direction và cường độ speed, giúp thiên 
        thạch bay theo hướng mong muốn.
    */
    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    } 

    // new

    private bool hasBeenHit = false; // Cờ theo dõi va chạm với Bullet
    /* Biến hasBeenHit trong code trên chỉ được áp dụng cho từng đối tượng (thiên thạch) riêng biệt, vì mỗi 
     * thiên thạch sẽ có một bản sao của biến này. Do đó, thay đổi giá trị của hasBeenHit cho một thiên thạch 
     * sẽ không ảnh hưởng đến các thiên thạch khác. Mỗi thiên thạch đều có trạng thái riêng biệt của nó. */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu tag là "Bullet" và đối tượng chưa bị va chạm trước đó
        if (collision.gameObject.tag == "Bullet" && !hasBeenHit)
        {
            // Đánh dấu là đã nhận Bullet
            hasBeenHit = true;

            // Nếu size của đối tượng lớn hơn minSize, thì tách thành 2 mảnh
            if ((this.size * 0.5f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            // Xóa đối tượng sau khi va chạm
            Destroy(this.gameObject);
        }
    }
    
    private void CreateSplit()
    {
        // lấy vị trí từ Object đã run hàm
        // Vector này là vector vị trí X Y
        Vector2 position = this.transform.position;

        /*
           Random.insideUnitCircle tạo ra 1 điẻm tọa độ ngẫu nhiên trong hình tròn bán kính 1.
           Khi nhân với 0.5f, nó thu nhỏ hình tròn về bán kính 0.5. 
           Từ position + 1 điểm tọa độ có bán kính 0.5 nữa thì kết quả ra 
           vector vị trí lệch trong phạm vị 0.5
        */
        position += Random.insideUnitCircle * 0.5f;

        /*
        🔹 Instantiate(this, position, this.transform.rotation);
                Instantiate(...): Hàm của Unity để tạo một bản sao (clone) của một GameObject.
                - this: Tham chiếu đến chính đối tượng hiện tại (Asteroid).
                - position: Vị trí mới mà bản sao sẽ được tạo ra (đã bị lệch một chút bằng Random.insideUnitCircle).
                - this.transform.rotation: Giữ nguyên góc quay (rotation) của đối tượng gốc.     
        */
        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
        /*Chọn một hướng ngẫu nhiên trong hình tròn đơn vị (bán kính = 1) và chuẩn hóa nó để có độ dài = 1.
        Chuẩn hóa vector sẽ đảm bảo rằng mọi hướng đều có tốc độ di chuyển giống nhau (vì độ dài vector bằng 1).
        Random.insideUnitCircle.normalized là một vector hướng (direction vector), chứ không phải một vector vị trí.
        */
    }
}


