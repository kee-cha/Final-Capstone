namespace Final_CapAPi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Final_CapAPi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Final_CapAPi.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Injuries.AddOrUpdate(

                new Models.Injury
                {
                    Type = "Headache",
                    Description = "A headache(medically termed cephalgia) is a pain in the head.Headaches can be located anywhere in the head, including above the eyes or the ears, behind the head(occipital headache), the top of the head(coronal headache), or in the back of the upper neck.Headache, like chest pain or backache, has many causes.",
                    Cause = "People do not know exactly what causes tension-type headaches, but factors that trigger them may include: physical or emotional stress, anxiety, depression, lack of sleep, lack of exercise, eye strain, dehydration, regular exposure to loud noise, tiredness, poor posture, jaw clenching, alcohol use",
                    Treatment= "Start by placing your thumbs on your cheekbones close to your ears, and use your fingertips to gently apply pressure and rub the temples (the soft spot between the corner of your eye and your ear). Using very firm pressure and a tiny circular motion, gradually move your fingers up along your hairline until they meet in the middle of your forehead, massaging your entire forehead and scalp as you inch along.",
                    InjuryLocation = "Head"
                },
                new Models.Injury
                {                
                    Type = "Nasal ",
                    Description = "Non-infectious conditions where there is inflammation of the nose cause nasal pain as well. Most types of non-infectious nasal inflammation (rhinitis), like allergic rhinitis (hayfever), do not cause pain in the nose but there may be other sensations like an itchy nose. Apart from injury to the nose as described above, mechanical and chemical irritation can both cause nose pain. It may include nose picking, sniffing cocaine, excessive nasal drying, very hard dried nasal mucus, inhaling irritant gases and using certain types of nasal sprays. Usually the pain is short lived in these cases.",
                    Cause = "We all know that a broken nose can be painful. The term ‘broken nose’ refers a fracture of one of the nasal bones. It is very painful and often there is swelling with/without bleeding of the nose. Nasal fractures often occur with trauma to the face, as may occur during sporting injuries, severe falls or as a result of being assaulted. Sometimes blood can pool in the septum after an injury. This is known as a nasal septal hematoma and it needs to be drained as soon as possible. Even without a fracture or hematoma, there may be nose sensitivity after injury.",
                    Treatment = "The sphenoid sinuses can be found on the side of the skull in the sphenoid bone, which is behind the nose and between the eyes, just below the pituitary gland. The ethmoid sinuses are located in the ethmoid bone, the bone that divides the nasal cavity from the brain. This technique will address both types of sinuses. Place your index fingers on the bridge of your nose. Find the area between your nasal bone and the corner of the eyes. Hold a firm pressure in that spot with your fingers for about 15 seconds. Then, using your index fingers, stroke downward along the side of the bridge of your nose. Repeat the slow downward strokes for about 30 seconds.",
                    InjuryLocation = "Head"
                },
                new Models.Injury
                {
                    Type = "Mandible",
                    Description = "Jaw pain, which sometimes radiates to other areas of the face, is a common concern. It can develop due to sinus infections, toothaches, issues with the blood vessels or nerves, or other conditions. Most types of jaw pain result from temporomandibular joint disorder.In many cases, jaw pain does not need immediate medical attention, but sometimes, it can indicate a more serious underlying condition that needs treatment.",
                    Cause = "Jaw pain can result from physical injuries, damage to the nerves or blood vessels, infections, and several other causes. Temporomandibular joint disorder is a cluster of conditions that affect the bones, joints, and muscles responsible for jaw movement. These conditions can cause pain and discomfort. It is a common complaint and usually goes away without medical treatment, though some types may need treatment.",
                    Treatment = "While massage may be of some benefit to people with TMJ, no type of massage therapy can be recommended as a principal treatment for this condition. Strategies such as applying hot and cold packs to the affected area may also offer TMJ relief, while alternative therapies like acupuncture and biofeedback show promise as natural TMJ treatments. It's crucial to address common triggers like teeth grinding, stress, and behavioral factors (such as excessive gum chewing) in the treatment of TMJ.",
                    InjuryLocation = "Head"
                },
                new Models.Injury
                {
                    Type = "Impingement",
                    Description = "Irritation or pinching of the roots of the spinal nerves causes pain that may be sharp, fleeting, severe, or accompanied by pins and needles. Depending on the nerve involved, the pain may shoot down the arm or even into the hand.",
                    Cause = "Usually the pain is caused by something simple, like hunching your shoulders over a keyboard or work surface. Posture can be another factor. Other causes include arthritis, whiplash, a pinched nerve, muscle strain or degenerative disease. Whether it's chronic or lasts only a short time, neck pain can be relieved by massage.",
                    Treatment = "Massage of the neck itself may exacerbate symptoms at first, but gentle massage of the arms, chest, legs and back proves to reduce pain in the neck. Myofascial release and thorough massage of all the neck, face, jaw, head and throat muscles, promote full healing and restoration of function.",
                    InjuryLocation = "Head"
                },
                new Models.Injury
                {
                    Type = "Strain/Sprain",
                    Description = "Back strain is damage to the tendons or muscles of the back. Sprains and strains are usually caused by overuse, such as excessive stretching or improperly lifting.",
                    Cause = "Muscle strain in the lower back or upper back/neck. Most episodes of acute lower back pain are caused by muscle strain, such as from lifting a heavy object, a sudden movement or a fall. The low back pain can be very severe and last for several hours, several days or even a few weeks. When back muscles are strained or torn, the area around the muscles can become inflamed. With inflammation, the muscles in the back can spasm and cause both severe lower back pain and difficulty moving. The large upper back muscles are also prone to irritation, either due to de-conditioning (lack of strength) or overuse injuries (such as repetitive motions). Upper back pain may also be due to a specific event, such as a muscle strain, sports injury, or auto accident.",
                    Treatment = "With neuromuscular therapy, the therapist applies alternating levels of concentrated pressure (10-30 seconds) on the areas of muscle spasm. The patient will feel some pain or discomfort from the pressure, but the muscle spasm should be lessened after the massage. Any soreness from the pressure should fade in 1 to 3 days, and the muscles that were worked should be less tight for a week or two afterwards. A typical massage therapy program for muscle spasms consists of four sessions over 6 weeks.",
                    InjuryLocation = "Back"
                },
                new Models.Injury
                {
                    Type = "Trauma",
                    Description = "Injuries from trauma (such as a car accident or falling) are common causes of back pain. A traumatic injury can overly compress the spine, leading to a herniated disc, or put pressure on spinal cord nerves, according to the National Institute of Neurological Disorders and Stroke.",
                    Cause = "Stress or injury involving the back muscles, including back sprain or strain; chronic overload of back muscles caused by obesity; and short term overload of back muscles caused by any unusual stress, such as lifting or pregnancy",
                    Treatment = "Massage can help alleviate the muscle spasms which occur when your rib or thoracic joints lose mobility, thereby helping those joints function better. It's even possible to increase the mobility of your sacroiliac joint or tailbone by massaging the gluteal muscles and releasing tension in the tissues. Along with enhancing other types of medical treatment, back massage therapy can provide significant relief of incidental or chronic pain.Whether chronic pain is due to limited mobility of the vertebral, pelvic and rib joints or to compression of nerves through muscle spasm, disease or structural dysfunction, it helps to find a professional therapist who's experienced in back massage.",
                    InjuryLocation = "Back"
                },
                new Models.Injury
                {
                    Type = "Spinal Deformity",
                    Description = "Spine deformity can happen when unnatural curvature occurs, as in scoliosis (side-to-side curvature) or kyphosis and Scheuermann's disease (front-to-back curvature). It also occurs due to defect (e.g. spondylolisthesis) or damage to the spine (if there are  multiple fractures or ankylosing spondylitis). Deformities do not commonly cause pain unless the change in structure restricts movement or reduces room in the spinal canal and puts pressure on the nerves there. ",
                    Cause = "In most cases, the cause behind scoliosis and kyphosis is unknown. The cause of both scoliosis and kyphosis is thought to be a combination of factors including abnormal development of the bones, soft ligaments or weak muscles, or abnormalities with the inner ear and balance functions. The resulting curvature of the spine affects all of the muscles in the back, as well as the alignment of the hip. The commonly noted symptoms of the curved-spine condition include: Uneven shoulders or waistline, One or both shoulder blades sticking out, Leaning slightly to one side, A hump on one side of the back", 
                    Treatment = "Chiropractors apply pressure to an area to align bones and return joints to a more normal motion. Patients with spinal deformity might benefit from tissue massage for a muscle spasm, traction for a pinched nerve, or ultrasound for tight muscles. Dry needling or acupuncture might also prove helpful. But most patients with spinal deformity are not candidates for a high-velocity spinal adjustment (a back crack). Such adjustments (by x-ray criteria) do not result in a measureable change of spinal alignment. Anyone with a major spinal deformity who is considering chiropractic treatment should consult with a neurosurgeon first to determine whether it is safe.",
                    InjuryLocation = "Back"
                },
                new Models.Injury
                {
                    Type = "Hernia",
                    Description = "A herniated disk refers to a problem with one of disks that sit between the individual vertebrae of the spine.  Sometimes called a slipped disk or a ruptured disk, a herniated disk occurs when some of the nucleus pushes out through a tear in the rubber exterior cover (Annulus). A herniated disk, which can occur in any part of the spine, can irritate a nearby nerve. Depending on where the herniated disk is, it can result in pain, numbness or weakness in an arm or leg.",
                    Cause = "Ultimately, all hernias are caused by a combination of pressure and an opening or weakness of muscle or fascia; the pressure pushes an organ or tissue through the opening or weak spot. Sometimes the muscle weakness is present at birth; more often, it occurs later in life.",
                    Treatment = "One of the benefits of Massage Therapy for Hernias is that it can relieve stress and tension in the area and help restore balance in that area. A licensed Hernia Massage Therapist understands the mechanics of hernias and can treat the condition by applying gentle friction at the site to help restore muscle balance, help prevent further structural distortions and bring about pain relief. In fact, studies show that massage is effective for treating pain, stress and muscle tension for a number of different conditions, including soft tissue injuries.",
                    InjuryLocation = "Back"
                },
                new Models.Injury
                {
                    Type = "Dislocation/Separation",
                    Description = "If your shoulder is pulled back too hard or rotated too far, the top of your arm might pop out of its socket. You will feel pain and weakness in your shoulder. You may also have swelling, numbness and bruising.",
                    Cause = "Dislocations typically result when a joint experiences an unexpected or unbalanced impact. This might happen if you fall or experience a harsh hit to the affected area. After a joint dislocates, it’s more likely to dislocate again in the future.",
                    Treatment = "Correcting a dislocated joint should only be done by someone skilled and trained to reduce dislocations, such as an orthopedist or physical therapist (this treatment is out of the scope of practice for massage practitioners). If performed improperly, serious injury can result in attempting to correct a dislocation. The brachial plexus and axillary artery are very close to the lip of the glenoid labrum and can be damaged or severed with an improper attempt to fix a dislocation. A massage therapist is rarely going to be around right after a dislocation has occurred.More likely, the massage therapist’s role is helping to manage the resultant soft - tissue reactions to the injury.Massage can address soft - tissue challenges such as impingement, tendon irritation, or biomechanical imbalance resulting from the injury.",
                    InjuryLocation = "Arm"
                },
                new Models.Injury
                {
                    Type = "Impingement",
                    Description = "This happens when the tendons of the rotator cuff get pinched in the bones of the shoulder. It can cause swelling and pain. If you lift your arms over your head a lot, it can set this off.",
                    Cause = "The nerve entrapment can be caused by tendons, ligaments, muscles or fascia that put unusual pressure on the nerve after a traumatic injury or repeated overuse. Nerve pain, numbness, or tingling may be localized or radiate to other nearby areas.",
                    Treatment = "Massage therapists will not only massage the muscles of these areas, but also provide stretching techniques to help release the impingement. Sometimes these stretches will be painful, as they require a shortening and lengthening of an already inflamed nerve.",
                    InjuryLocation = "Arm"
                },
                new Models.Injury
                {
                    Type = "Inflamation",
                    Description = "Tendonitis is inflammation of the tendon. It commonly occurs in the shoulders, elbows, and wrists. Tendonitis can vary from mild to severe. Other symptoms include mild swelling, tenderness, and a dull, aching pain.",
                    Cause = "Arm inflammation can be caused by a number of medical conditions, particularly autoimmune disorders, or it can be the result of injury. Rheumatoid arthritis is one common culprit that causes inflammation in the joints of the arms. Inflammation of the blood vessels, a condition known as vasculitis, can also affect the arms, particularly in patients with Buerger's disease. Other diseases that can affect the arms include scleroderma, lupus, and dermatomyositis. Injuries may be the result of a single incident that causes strain or damage, from overuse, or from repetitive motion and can cause inflammation in the bones or joints, muscles, or tendons and ligaments.",
                    Treatment = "Massage therapy can very often be beneficial for shoulder pain, if not directly, then by releasing muscle restrictions in the surrounding tissue which allows for free range of motion in the joint. By lengthening muscle fibers, releasing trigger points and stretching tendons with range of motion stretches, we can improve function in the shoulder joint and help to reduce pain for our clients.",
                    InjuryLocation = "Arm"
                },
                new Models.Injury
                {
                    Type = "Strain/Sprain ",
                    Description = " Hand pain is one feature of joint inflammation (arthritis) that may be felt in the hand. Rheumatoid arthritis and osteoarthritis are the two most common types of arthritis in the hand. Repetitive motion injuries, including carpal tunnel syndrome, can cause pain in the wrist and hand.",
                    Cause = "Hand pain can be caused by disease or injury affecting any of the structures in the hand, including the bones, muscles, joints, tendons, blood vessels, or connective tissues.",
                    Treatment = "When massaging injured area, apply light pressure to reduce swelling and tension. Focus deeper pressure the origin or belly of the muscle. These areas are located superior to the affected areas in the hand or wrist. Doing so will relieve the overall tension of the muscle. Don't forget to use RICE(Rest, Ice, Compression, Elevation)",
                    InjuryLocation = "Arm"
                },
                new Models.Injury
                {
                    Type = "Impingement",
                    Description = "Sciatica is pain in the lower extremity resulting from irritation of the sciatic nerve. The pain of sciatica is typically felt from the low back (lumbar area) to behind the thigh and can radiate down below the knee. ",
                    Cause = "Sciatica occurs when the sciatic nerve becomes pinched, usually by a herniated disk in your spine or by an overgrowth of bone (bone spur) on your vertebrae. More rarely, the nerve can be compressed by a tumor or damaged by a disease such as diabetes.",
                    Treatment = "To reduce muscle tension or spasm that cause impingement on the sciatic nerve, massage around the gluteus medius and maximus. Along the sacrum, apply deeper pressure to massage the piriformis muscle which would be the main culprit in impingement of the sciatic nerve.",
                    InjuryLocation = "Hip"
                },
                new Models.Injury
                {
                    Type = "Dislocation/Separation",
                    Description = "A hip dislocation is a very unusual injury that is most often the result of severe trauma.   People who sustain this injury will have severe hip pain, difficulty moving, and inability to bear weight on the extremity.The leg will be in an abnormal position as a result of the dislocation, most often with the leg shortened and rotated.",
                    Cause = "The most common causes of a hip dislocation include motor vehicle collisions, falls from a height, and sometimes catastrophic sports injuries.",
                    Treatment = "The most important treatment of a dislocated hip is to properly position the ball back in the socket, called a joint reduction.2? In order to reposition the hip joint, the patient will require general anesthesia. Unlike a shoulder dislocation that many patients, especially those who have had repeat shoulder dislocations, can reposition on their own, a hip dislocation usually requires significant force to reposition.",
                    InjuryLocation = "Hip"
                },
                new Models.Injury
                {
                    Type = "Tear",
                    Description = "A hip labral tear involves the ring of cartilage (labrum) that follows the outside rim of the socket of your hip joint. In addition to cushioning the hip joint, the labrum acts like a rubber seal or gasket to help hold the ball at the top of your thighbone securely within your hip socket. ",
                    Cause = "Trauma. Injury to or dislocation of the hip joint — which can occur during car accidents or from playing contact sports such as football or hockey — can cause a hip labral tear.",
                    Treatment = "Often hip arthroscopy is a treatment option. Massaging injured area is beneficial post surgery to assist in scar tissue removal and reducing muscle tightness.",
                    InjuryLocation = "Hip"
                },
                new Models.Injury
                {
                    Type = "Strain/Sprain",
                    Description = " Strains of the muscles around the hip and pelvis can cause pain and spasm. The most common strains are groin pulls and hamstring strains.",
                    Cause = "Hip flexor strain occurs when you use your hip flexor muscles and tendons too much. As a result, the muscles and tendons become inflamed, sore, and painful. Some people are more likely than others to experience hip flexor strain. ",
                    Treatment = "Massage therapy helps in breaking down the adhesions in the hip regions thereby reducing pain and restoring normal hip joint mobility. During the early stage of the massage therapy, the individuals may feel pain and discomfort, which will gradually subside as the frequency of the massage therapy increases.",
                    InjuryLocation = "Hip"
                },
                new Models.Injury
                {
                    Type = "Trauma",
                    Description = " Some fractures are easily and immediately visible, with severe bruising, swelling, and deformation. These normally receive urgent medical attention. ",
                    Cause = "Heavy pressure, for example, from a fall, can lead to fractures.  Stress fractures are small fractures that can result from repetitive stresses sustained during sports, often when the intensity of activity increases too quickly.",
                    Treatment = "Fractures should not be massage and to have a professional medical team to provide treatment. Once injury is healed, apply range of motion techniques to help increase mobility. Light massage can be used to increase blood circulation as well as reduce any tension.",
                    InjuryLocation = "Leg"
                },
                new Models.Injury
                {
                    Type = "Impingement",
                    Description = "Sciatica happens when pressure is put on a nerve, often in the spine, leading to pains that run down the leg from the hip to the foot. ",
                    Cause = "It can happen when a nerve is 'pinched' in a muscle spasm or by a herniated disk. Long-term effects include strain on other parts of the body as the gait changes to compensate for the pain.",
                    Treatment = "To reduce muscle tension or spasm that cause impingement on the sciatic nerve, massage around the gluteus medius and maximus. Along the sacrum, apply deeper pressure to massage the piriformis muscle which would be the main culprit in impingement of the sciatic nerve.",
                    InjuryLocation = "Leg"
                },
                new Models.Injury
                {
                    Type = "Strain/Sprain",
                    Description = "Acute trauma can lead to sprains and strains. A sprain refers to a stretching or tearing. A strain is an injury to the muscles or tendons. ften associated with running, a hamstring strain can lead to acute pain in the rear of the thigh muscle, usually due to a partial tear. ",
                    Cause = "Sprains and strains usually develop because of inadequate flexibility training, overstretching, or not warming up before an activity.Continuing to exercise while injured increases the risk.",
                    Treatment = "Massaging should not be done directly on acute sprains/strains areas. Doing so can slow down recovery time. However, massaging around the injured area can be beneficial in recovery in combination with ice compressions. Light touch can be applied directly on injured area to assist in reducing swelling by moving the blood away from area. ",
                    InjuryLocation = "Leg"
                },
                new Models.Injury
                {
                    Type = "Inflamation",
                    Description = "Popliteus tendinitis produced knee pain during downhill running. ",
                    Cause = "It is caused by inflammation of the popliteus tendon, which is important for knee stability.",
                    Treatment = "Use RICE(Rest, Ice, Compression, Elevation) to help reduce inflamation. Massage the belly(center) of the popliteal muscle located behind the knee to reduce tension.",
                    InjuryLocation = "Leg"
                }
            );
           
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
