// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.




// window.onbeforeunload = function() {
//     if (dirty) {
//        console.log("dirty !");
//        return "Do you really want to leave our brilliant application?";
//    } else {
//         console.log("not dirty !");
//       return;
//    }
// };
var dirty = false;

function setDirty(dirtyvar)
{
    dirty = dirtyvar;
    $(".save").prop("disabled",!dirtyvar);
}

function SearchStudent() {
    var input, filter, ul, li, a, i, txtValue;
    input = document.getElementById("SearchInput");
    filter = input.value.toUpperCase();
    ul = document.getElementById("studUL");
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];
        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}

// Write your JavaScript code.
function open_kpi_arch(div){
    const p = div.parentElement.parentElement.querySelectorAll('p');
    p.forEach((l) => {
        if(l.style.display === "block"){
            l.style.display = "none";
            div.style.transform = "rotate(-90deg)";
        }else{
            l.style.display = "block";
            div.style.transform = "rotate(0deg)";
        }
    });
}

function config_teacher()
{
    const medal = document.querySelectorAll('.medal');
    medal.forEach(med =>
        med.addEventListener("click", function() {
            const medKPI = med.getAttribute('data-kpi-point');
            setDirty(true);
            switch(String(medKPI)) {
                case 'null':
                    med.setAttribute('data-kpi-point', '1');
                    break;
                case '0':
                    med.setAttribute('data-kpi-point', '1');
                    break;
                case '1':
                    med.setAttribute('data-kpi-point', '2');
                    break;
                default:
                    med.removeAttribute('data-kpi-point');
            }
        }));
}

function config_student()
{
    const medal = document.querySelectorAll('.medal');
    medal.forEach(med =>
        med.addEventListener("click", function() {
            const medKPI = med.getAttribute('data-kpi-point');
            setDirty(true);
            switch(String(medKPI)) {
                case '2':
                    break;
                case 'null':
                    med.setAttribute('data-kpi-point', '1');
                    break;
                case '0':
                    med.setAttribute('data-kpi-point', '1');
                    break;
                default:
                    med.removeAttribute('data-kpi-point');
            }
        }));
}




function save_kpi(){
    // Check browser support
    if (typeof(Storage) !== "undefined") {
        // Store
        console.log(document.getElementById("body_kpi_tool").innerHTML);
        localStorage.setItem("KPI_table", document.getElementById("body_kpi_tool").innerHTML);
    } else {
        document.getElementById("error").innerHTML = "Sorry, your browser does not support Web Storage...";
    }
}

function download_kpi(){

    print();

}




function submit_kpi(){
    const all_kpi = document.querySelectorAll('.desc >p');
    const all_kpi_json = {};
    let NUM = 0;

    all_kpi.forEach((kpi) =>{
        const data_kpi = kpi.getAttribute('data-kpi');
        const data_arch = kpi.parentElement.getAttribute('data-arch');

        switch(String(data_kpi)) {
            case '1':
                //console.log(1, NUM, data_arch);
                NUM++;
                all_kpi_json[NUM] = {"LAYER": data_arch, "decr": kpi.innerHTML };
                break;

            case '2':
                //console.log(2, NUM, data_arch);
                NUM++;
                all_kpi_json[NUM] = {"LAYER": data_arch, "decr": kpi.innerHTML };
                break;

            default:

        }

    });

    alert("SEND TO DATA BASE:\n" + JSON.stringify(all_kpi_json));
}

function open_kpi(){
    // Check browser support
    if (typeof(Storage) !== "undefined") {
        //console.log(localStorage.getItem("KPI_table"));
        //if( localStorage.getItem("KPI_table")){
        document.getElementById("body_kpi_tool").innerHTML = localStorage.getItem("KPI_table");
        config();
        //}
    } else {
        document.getElementById("error").innerHTML = "Sorry, your browser does not support Web Storage...";
    }
}