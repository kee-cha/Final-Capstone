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
                    Cause ="",
                    Treatment="",
                    InjuryLocation = "Head"
                },
                new Models.Injury
                {                
                    Type = "Nasal ",
                    Description = "Non-infectious conditions where there is inflammation of the nose cause nasal pain as well. Most types of non-infectious nasal inflammation (rhinitis), like allergic rhinitis (hayfever), do not cause pain in the nose but there may be other sensations like an itchy nose. Apart from injury to the nose as described above, mechanical and chemical irritation can both cause nose pain. It may include nose picking, sniffing cocaine, excessive nasal drying, very hard dried nasal mucus, inhaling irritant gases and using certain types of nasal sprays. Usually the pain is short lived in these cases.",
                    Cause = "",
                    Treatment = "",
                    InjuryLocation = "Head"
                },
                new Models.Injury
                {
                    Type = "Mandible",
                    Description = "Jaw pain, which sometimes radiates to other areas of the face, is a common concern. It can develop due to sinus infections, toothaches, issues with the blood vessels or nerves, or other conditions. Most types of jaw pain result from temporomandibular joint disorder.In many cases, jaw pain does not need immediate medical attention, but sometimes, it can indicate a more serious underlying condition that needs treatment.",
                    Cause = "",
                    Treatment = "",
                    InjuryLocation = "Head"
                },
                new Models.Injury
                {
                    Type = "Impingement",
                    Description = "Irritation or pinching of the roots of the spinal nerves causes pain that may be sharp, fleeting, severe, or accompanied by pins and needles. Depending on the nerve involved, the pain may shoot down the arm or even into the hand.",
                    Cause = "",
                    Treatment = "",
                    InjuryLocation = "Head"
                },
                new Models.Injury
                {
                    Type = "Strain/Sprain",
                    Description = "Back strain is damage to the tendons or muscles of the back. Sprains and strains are usually caused by overuse, such as excessive stretching or improperly lifting.",
                    Cause = "",
                    Treatment = "",
                    InjuryLocation = "Back"
                },
                new Models.Injury
                {
                    Type = "Trauma",
                    Description = "Injuries from trauma (such as a car accident or falling) are common causes of back pain. A traumatic injury can overly compress the spine, leading to a herniated disc, or put pressure on spinal cord nerves, according to the National Institute of Neurological Disorders and Stroke.",
                    Cause = "",
                    Treatment = "",
                    InjuryLocation = "Back"
                },
                new Models.Injury
                {
                    Type = "Spinal Deformity",
                    Description = "Spine deformity can happen when unnatural curvature occurs, as in scoliosis (side-to-side curvature) or kyphosis and Scheuermann's disease (front-to-back curvature). It also occurs due to defect (e.g. spondylolisthesis) or damage to the spine (if there are  multiple fractures or ankylosing spondylitis). Deformities do not commonly cause pain unless the change in structure restricts movement or reduces room in the spinal canal and puts pressure on the nerves there. ",
                    Cause = "",
                    Treatment = "",
                    InjuryLocation = "Back"
                },
                new Models.Injury
                {
                    Type = "Hernia",
                    Description = "A herniated disk refers to a problem with one of disks that sit between the individual vertebrae of the spine.  Sometimes called a slipped disk or a ruptured disk, a herniated disk occurs when some of the nucleus pushes out through a tear in the rubber exterior cover (Annulus). A herniated disk, which can occur in any part of the spine, can irritate a nearby nerve. Depending on where the herniated disk is, it can result in pain, numbness or weakness in an arm or leg.",
                    Cause = "",
                    Treatment = "",
                    InjuryLocation = "Back"
                },
                new Models.Injury
                {
                    Type = "Dislocation/Separation",
                    Description = "If your shoulder is pulled back too hard or rotated too far, the top of your arm might pop out of its socket. You will feel pain and weakness in your shoulder. You may also have swelling, numbness and bruising.",
                    Cause = "",
                    Treatment = "",
                    InjuryLocation = "Arm"
                },
                new Models.Injury
                {
                    Type = "Impingement",
                    Description = "This happens when the tendons of the rotator cuff get pinched in the bones of the shoulder. It can cause swelling and pain. If you lift your arms over your head a lot, it can set this off.",
                    Cause = "",
                    Treatment = "",
                    InjuryLocation = "Arm"
                },
                new Models.Injury
                {
                    Type = "Inflamation",
                    Description = "Tendonitis is inflammation of the tendon. It commonly occurs in the shoulders, elbows, and wrists. Tendonitis can vary from mild to severe. Other symptoms include mild swelling, tenderness, and a dull, aching pain.",
                    Cause = "",
                    Treatment = "",
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
                    Treatment = "",
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
                    Cause = "",
                    Treatment = "",
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
