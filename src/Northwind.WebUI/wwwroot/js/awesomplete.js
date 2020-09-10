document.addEventListener('DOMContentLoaded', () => {
  
  document.querySelectorAll('input.awesomplete').forEach((el) => {
    const awesome = new Awesomplete(el);

    el.addEventListener(`keyup`, (e) => {

      if(['Enter', 'Escape', 'ArrowDown', 'ArrowUp'].includes(e.key)) {
        return;
      }

      // const selector = 'data-html5-action-2';
      // const action = el.attributes[selector].value;
      // const additionalFields = el.getAttribute('data-html5-additional-fields-2');
      // const frm = el.closest('form');
      // let urlSuffix = '';
      // if(additionalFields !== null) {
      //   const fields = additionalFields.split(',');
      //   fields.forEach((fieldName) => {
      //     const field = frm.querySelector(`#${fieldName}`);
      //     if(field !== null) {
      //       urlSuffix += `&${fieldName}=${field.value}`;
      //     }
      //   });
      // }
      // const url = `/${action}?${el.id}=${e.currentTarget.value}${urlSuffix}`;

      const url = `/suppliers/idsearch?companyName={}`;

      fetch(url).then((resp) => resp.json()).then((json) => {
        let list = [];
        json.forEach((item) => list.push({ label: item.key, value: item.value }));
        awesome.list = list;
      });

    });

  });

});